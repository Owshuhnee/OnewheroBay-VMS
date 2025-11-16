using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using VMS.WebApp.Models;

namespace VMS.WebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DBSET HERE

        // These DbSet properties represent the tables in the database.
        // Each DbSet maps a model class to a database table so EF Core can
        // query, insert, update, and delete records for that entity.

        public DbSet<Visitor> Visitors { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<EventSession> EventSessions { get; set; } = null!;


        // MODEL BUILDER HERE
        // Configure entity-to-table mappings and column settings.
        // This section customizes how each model maps to the database schema,
        // including table names, column names, relationships, and constraints.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // USERS TABLE
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

            // EVENTS TABLE
            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("events");  // Postgres table name

                entity.HasKey(e => e.EventID);

                entity.Property(e => e.EventID)
                      .HasColumnName("event_id");

                entity.Property(e => e.EventName)
                      .HasColumnName("title");

                entity.Property(e => e.Description)
                      .HasColumnName("description");

                entity.Property(e => e.Location)
                      .HasColumnName("location");

                entity.Property(e => e.IsActive)
                      .HasColumnName("is_active");

                entity.HasMany(e => e.Sessions) // 1 Event -> many Event Sessions
                      .WithOne(s => s.Event)
                      .HasForeignKey(s => s.EventID);

            });

            // EVENT SESSIONS TABLE
            modelBuilder.Entity<EventSession>(entity =>
            {
                entity.ToTable("event_sessions"); // Postgres table name

                entity.HasKey(e => e.SessionID);

                entity.Property(e => e.SessionID)
                      .HasColumnName("session_id");

                entity.Property(e => e.EventID)
                      .HasColumnName("event_id");

                entity.Property(e => e.Date)
                      .HasColumnName("date");

                entity.Property(e => e.StartTime)
                      .HasColumnName("start_time");

                entity.Property(e => e.Capacity)
                      .HasColumnName("capacity");

                entity.Property(e => e.Price)
                      .HasColumnName("price");

                entity.Property(e => e.IsActive)
                      .HasColumnName("is_active");
            });


            // ADD Visitor / Booking mappings here later. Visitors
        }
    }
}
