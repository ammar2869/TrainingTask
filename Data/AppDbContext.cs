using Microsoft.EntityFrameworkCore;
using TraineeManagement.Models;
using TraineeManagement.Models.Entities;

namespace TraineeManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trainee> Trainees { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Mentor> Mentors {get; set;}
        public DbSet<LearningTask> LearningTasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username)
                      .IsUnique();

                entity.Property(u => u.Role).HasConversion<string>();

                // entity.Property(u => u.CreatedDate)
                //       .HasDefaultValueSql("CURRENT_TIMESTAMP");

                // entity.Property(u => u.UpdatedDate)
                //       .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Mentor>(entity =>
            {
                entity.HasIndex(m => m.Email).IsUnique();

                // entity.Property(u => u.CreatedDate)
                //     .HasDefaultValueSql("CURRENT_TIMESTAMP")
                //     .ValueGeneratedOnAdd();

                // entity.Property(u => u.UpdatedDate)
                //     .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP")
                //     .ValueGeneratedOnAddOrUpdate();
            });
            

            base.OnModelCreating(modelBuilder);
        }
    }
}