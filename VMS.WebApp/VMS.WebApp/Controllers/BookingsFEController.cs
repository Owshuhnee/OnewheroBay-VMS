using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using VMS.WebApp.Data;
using VMS.WebApp.Models;

namespace VMS.WebApp.Controllers
{
    public class BookingsFEController : Controller
    {
        private readonly AppDbContext _context;

        public BookingsFEController(AppDbContext context)
        {
            _context = context;
        }

        private int? GetCurrentUserId()
        {
            return HttpContext.Session.GetInt32("UserID");
        }

        // GET: /BookingsFE
        // This shows your booking page with event card (left) + booking form (right)
        public async Task<IActionResult> Index(int? eventId)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Home");
            }

            Event? evt = null;

            if (eventId.HasValue)
            {
                evt = await _context.Events
                    .FirstOrDefaultAsync(e => e.EventID == eventId.Value && e.IsActive);
            }

            var model = new BookingStartViewModel
            {
                EventID = evt?.EventID ?? 0,
                EventName = evt?.EventName ?? "Select an event to book",
                Description = evt?.Description ?? "Please choose an event from the Events page.",
                EventImage = evt?.EventImage,
                Schedule = evt?.Schedule,
                TicketPrice = evt?.TicketPrice,
                AvailableSlots = evt?.AvailableSlots,
                PreferredDate = DateTime.Today,
                PreferredTime = new TimeSpan(9, 0, 0),
                GuestCount = 1
            };

            return View(model);
        }

       // Later, you can add a POST here to actually save the booking.
    }
}
