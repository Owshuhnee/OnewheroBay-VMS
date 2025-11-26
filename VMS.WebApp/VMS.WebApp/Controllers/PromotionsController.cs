using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.WebApp.Data;
using VMS.WebApp.Models;

namespace VMS.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PromotionsController(AppDbContext context)
        {
            _context = context;
        }

        // Small DTOs so we don’t return full EF entities
        public class PromotionCustomerDto
        {
            public int UserId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }

        public class PopularEventWithCustomersDto
        {
            public int EventId { get; set; }
            public string EventName { get; set; } = string.Empty;
            public int BookingCount { get; set; }
            public List<PromotionCustomerDto> Customers { get; set; } = new();
        }

        /// <summary>
        /// Returns the top N events with booking counts
        /// and the distinct customers who booked each event.
        /// Example: GET /api/promotions/popular-events?top=5
        /// </summary>
        [HttpGet("popular-events")]
        public async Task<IActionResult> GetPopularEventsWithCustomers([FromQuery] int top = 5)
        {
            // For now: simple implementation that loads bookings then groups in memory.
            // This is fine while data volumes are small.
            var allBookings = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .Where(b => b.Event != null && b.User != null)
                .ToListAsync();

            var grouped = allBookings
                .GroupBy(b => b.EventId)
                .OrderByDescending(g => g.Count())
                .Take(top);

            var result = grouped.Select(g =>
            {
                var first = g.First();

                // Distinct customers for this event
                var customers = g
                    .GroupBy(b => b.User!.UserId)
                    .Select(ug => new PromotionCustomerDto
                    {
                        UserId = ug.Key,
                        Name = $"{ug.First().User!.FirstName} {ug.First().User!.LastName}",
                        Email = ug.First().User!.Email
                    })
                    .ToList();

                return new PopularEventWithCustomersDto
                {
                    EventId = first.Event!.EventId,
                    EventName = first.Event.EventName,
                    BookingCount = g.Count(),
                    Customers = customers
                };
            }).ToList();

            return Ok(result);
        }
    }
}
