using Microsoft.EntityFrameworkCore;
using TraineeManagement.Data;
using TraineeManagement.Services;
using System.Text.Json.Serialization;
using TraineeManagement.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddScoped<ITraineeService, TraineeService>();

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseInMemoryDatabase("TraineeManagementDb"));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    ));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.RoutePrefix = string.Empty;
    });

    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Users.Any())
    {
        db.Users.Add(new User
        {
            Username = "admin",
            Email = "admin@test.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            Role = "Admin",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        });

        db.SaveChanges();
    }
}

app.Run();