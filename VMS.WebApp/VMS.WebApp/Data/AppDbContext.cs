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

        public DbSet<Visitor> Visitors { get; set; } = null!;

        public DbSet<Event> Events { get; set; } = null!;

        public DbSet<Booking> Bookings { get; set; } = null!;
    }
}
