using Microsoft.EntityFrameworkCore;
using TraineeManagement.Data;
using TraineeManagement.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddScoped<ITraineeService, TraineeService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TraineeManagementDb"));

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

app.Run();