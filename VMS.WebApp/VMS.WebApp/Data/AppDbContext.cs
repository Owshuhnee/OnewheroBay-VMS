using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VMS.WebApp.Models;

namespace VMS.WebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // THIS IS TO MAP THE CLASSES TO THE TABLES

        public DbSet<Visitor> Visitors { get; set; } = null!;

        public DbSet<Event> Events { get; set; } = null!;

        public DbSet<Booking> Bookings { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // FOR USERS TABLE
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");   // Postgres table name

                entity.HasKey(u => u.UserId);

                entity.Property(u => u.UserId)
                      .HasColumnName("user_id");

                entity.Property(u => u.Role)
                      .HasColumnName("role");

                entity.Property(u => u.FirstName)
                      .HasColumnName("first_name");

                entity.Property(u => u.LastName)
                      .HasColumnName("last_name");

                entity.Property(u => u.Phone)
                      .HasColumnName("phone");

                entity.Property(u => u.Email)
                      .HasColumnName("email");

                entity.Property(u => u.Password)
                      .HasColumnName("password");

                entity.Property(u => u.CreatedDate)
                      .HasColumnName("created_date");

                entity.Property(u => u.IsActive)
                      .HasColumnName("is_active");
            });

        }
    }
}
