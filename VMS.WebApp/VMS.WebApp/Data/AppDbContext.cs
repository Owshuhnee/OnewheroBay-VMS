using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using VMS.WebApp.Models;

namespace VMS.WebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DBSET HERE

        // These DbSet properties represent the tables in the database.
        // Each DbSet maps a model class to a database table so EF Core can
        // query, insert, update, and delete records for that entity.

        public DbSet<Visitor> Visitors { get; set; } = null!; // Not using anymore, need to debug when have time
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;

        
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
                    entity.Property(u => u.UserId).HasColumnName("user_id");
                    entity.Property(u => u.Role).HasColumnName("role");
                    entity.Property(u => u.FirstName).HasColumnName("first_name");
                    entity.Property(u => u.LastName).HasColumnName("last_name");
                    entity.Property(u => u.Phone).HasColumnName("phone");
                    entity.Property(u => u.Email).HasColumnName("email");
                    entity.Property(u => u.Password).HasColumnName("password");
                    entity.Property(u => u.CreatedDate).HasColumnName("created_date");
                    entity.Property(u => u.IsActive).HasColumnName("is_active");
            });

            // EVENTS TABLE
            modelBuilder.Entity<Event>(entity =>
            {

                entity.ToTable("events");  // Postgres table name

                entity.HasKey(e => e.EventId);

                // Columns
                    entity.Property(e => e.EventId).HasColumnName("event_id");
                    entity.Property(e => e.EventName).HasColumnName("event_name");
                    entity.Property(e => e.Description).HasColumnName("description");
                    entity.Property(e => e.EventImage).HasColumnName("event_image");
                    entity.Property(e => e.IsActive).HasColumnName("is_active");
                    entity.Property(e => e.TicketPrice).HasColumnName("ticket_price");
                    entity.Property(e => e.AvailableSlots).HasColumnName("available_slots");
                    entity.Property(e => e.Schedule).HasColumnName("schedule");
                    entity.Property(e => e.Interest).HasColumnName("interest");

                // We are NOT using Location or Sessions anymore:
                // entity.Property(e => e.Location).HasColumnName("location");
                // entity.HasMany(e => e.Sessions)...  // removed
            });


            // Removed Event Sessions for now
            // Event(parent) directly to Booking(child)
            /* EVENT SESSIONS TABLE
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
            */

            // BOOKING TABLE
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("bookings"); // Postgres table name

                entity.HasKey(b => b.BookingId);

                // Columns
                entity.Property(b => b.BookingId).HasColumnName("booking_id");
                entity.Property(b => b.UserId).HasColumnName("user_id");
                entity.Property(b => b.EventId).HasColumnName("event_id");
                entity.Property(b => b.BookingDate).HasColumnName("booking_date");
                entity.Property(b => b.BookingTime).HasColumnName("booking_time");
                entity.Property(b => b.TotalPrice).HasColumnName("total_price");
                entity.Property(b => b.GuestCount).HasColumnName("guest_count");
                entity.Property(b => b.SpecialRequest).HasColumnName("special_request");
                entity.Property(b => b.BookingStatus).HasColumnName("booking_status");

                entity.HasOne(b => b.Event)
                       .WithMany(e => e.Bookings)
                       .HasForeignKey(b => b.EventId);

                entity.HasOne(b => b.User)
                      .WithMany()                 // or .WithMany(u => u.Bookings)
                      .HasForeignKey(b => b.UserId);

                entity.Ignore("VisitorID");
            });


            // ADD Visitor / Booking mappings here later. Visitors
        }
    }
}