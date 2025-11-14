using Microsoft.EntityFrameworkCore;
using VMS.WebApp.Models;

namespace VMS.WebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Visitor> Visitors { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        // public DbSet<User> Penguin { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map entity classes to actual table names (CASE-SENSITIVE!)
            modelBuilder.Entity<Event>().ToTable("events");        // lowercase in database
            modelBuilder.Entity<Visitor>().ToTable("Visitors");    // Capital V in database
            modelBuilder.Entity<Booking>().ToTable("Bookings");    // Capital B in database
            modelBuilder.Entity<User>().ToTable("users");          // lowercase in database
                                                               
            // ===== EVENTS TABLE MAPPINGS =====
            modelBuilder.Entity<Event>()
                .Property(e => e.EventID).HasColumnName("event_id");
            modelBuilder.Entity<Event>()
                .Property(e => e.EventName).HasColumnName("title");
            modelBuilder.Entity<Event>()
                .Property(e => e.EventDate).HasColumnName("event_date");
            modelBuilder.Entity<Event>()
                .Property(e => e.AvailableTickets).HasColumnName("available_tickets");
            modelBuilder.Entity<Event>()
                .Property(e => e.TicketPrice).HasColumnName("ticket_price");
            modelBuilder.Entity<Event>()
                .Property(e => e.Description).HasColumnName("description");
            modelBuilder.Entity<Event>()
                .Property(e => e.Location).HasColumnName("location");

            // ===== BOOKINGS TABLE MAPPINGS =====
            modelBuilder.Entity<Booking>().Property(b => b.BookingID).HasColumnName("BookingID");
            modelBuilder.Entity<Booking>().Property(b => b.VisitorID).HasColumnName("VisitorID");
            modelBuilder.Entity<Booking>().Property(b => b.EventID).HasColumnName("EventID");
            modelBuilder.Entity<Booking>().Property(b => b.BookingDate).HasColumnName("BookingDate");
            modelBuilder.Entity<Booking>().Property(b => b.UserID).HasColumnName("UserID");
            modelBuilder.Entity<Booking>().Property(b => b.TotalPrice).HasColumnName("TotalPrice");
            modelBuilder.Entity<Booking>().Property(b => b.Status).HasColumnName("Status");
            // ===== VISITORS TABLE MAPPINGS =====
            modelBuilder.Entity<Visitor>()
                .Property(v => v.VisitorID).HasColumnName("VisitorID");
            modelBuilder.Entity<Visitor>()
                .Property(v => v.FirstName).HasColumnName("FirstName");
            modelBuilder.Entity<Visitor>()
                .Property(v => v.LastName).HasColumnName("LastName");
            modelBuilder.Entity<Visitor>()
                .Property(v => v.Email).HasColumnName("Email");
            modelBuilder.Entity<Visitor>()
                .Property(v => v.Phone).HasColumnName("Phone");
            modelBuilder.Entity<Visitor>()
                .Property(v => v.Interests).HasColumnName("Interests");
            modelBuilder.Entity<Visitor>()
                .Property(v => v.RegistrationDate).HasColumnName("RegistrationDate");

            // ===== USERS TABLE MAPPINGS =====
            // Note: You'll need to verify the actual column names in users table
            modelBuilder.Entity<User>()
                .Property(u => u.UserId).HasColumnName("UserId");
            // Add other User column mappings as needed
        }
    }
}