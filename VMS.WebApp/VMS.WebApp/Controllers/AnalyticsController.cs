using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VMS.WebApp.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace VMS.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnalyticsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/analytics/stats
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            // Count users with 'customer' role (visitors)
            var totalVisitors = await _context.Users
                .Where(u => u.Role == "customer")
                .CountAsync();
            var activeBookings = await _context.Bookings.CountAsync();

            var now = DateTime.UtcNow;
            var startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var endOfMonth = startOfMonth.AddMonths(1);

            var thisMonthBookings = await _context.Bookings
                .Where(b => b.BookingDate >= startOfMonth && b.BookingDate < endOfMonth)
                .CountAsync();

            var revenue = await _context.Bookings
                .Where(b => b.BookingDate >= startOfMonth && b.BookingDate < endOfMonth)
                .SumAsync(b => (decimal?)b.TotalPrice) ?? 0;

            // Count upcoming bookings (next 30 days)
            var upcomingBookings = await _context.Bookings
                .Where(b => b.BookingDate > now && b.BookingDate <= now.AddDays(30))
                .CountAsync();

            // Count completed bookings this month
            var completedBookings = await _context.Bookings
                .Where(b => b.BookingDate >= startOfMonth && b.BookingDate < now)
                .CountAsync();

            return Ok(new
            {
                totalVisitors,
                activeBookings,
                thisMonthBookings,
                revenue,
                upcomingBookings,
                completedBookings 
            });
        }

        // GET: api/analytics/recent-bookings
        [HttpGet("recent-bookings")]
        public async Task<IActionResult> GetRecentBookings()
        {
            var recentBookings = await _context.Bookings
                .Include(b => b.Visitor)
                .Include(b => b.Event)
                .OrderByDescending(b => b.BookingDate)
                .Take(5)
                .Select(b => new
                {
                    id = b.BookingID,
                    name = b.Visitor != null ? b.Visitor.FirstName + " " + b.Visitor.LastName : "Unknown",
                    eventName = b.Event != null ? b.Event.EventName : "Unknown Event",
                    date = b.BookingDate.ToString("dd-MM-yyyy"),
                    status = b.Status ?? "Confirmed"  // Use actual Status from database
                })
                .ToListAsync();

            return Ok(recentBookings);
        }

        // GET: api/analytics/popular-events
        [HttpGet("popular-events")]
        public async Task<IActionResult> GetPopularEvents()
        {
            var popularEvents = await _context.Events
                .Include(e => e.Bookings)
                .OrderByDescending(e => e.Bookings.Count)
                .Take(4)
                .Select(e => new
                {
                    name = e.EventName,
                    bookings = e.Bookings.Count
                })
                .ToListAsync();

            return Ok(popularEvents);
        }
    }
}