using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using VMS.WebApp.Data;
using VMS.WebApp.Models;
using System.Linq;
using QRCoder;
using System.IO;

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

        // GET: /BookingsFE?eventId=2
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

        // POST: /BookingsFE/Start
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Start(BookingStartViewModel model)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var evt = await _context.Events
                .FirstOrDefaultAsync(e => e.EventID == model.EventID && e.IsActive);

            if (evt == null)
            {
                return NotFound("Event not found.");
            }

            var price = evt.TicketPrice ?? 0m;
            var totalPrice = price * model.GuestCount;

            var booking = new Booking
            {
                UserId = userId.Value,
                EventId = evt.EventID,
                BookingDate = DateTime.SpecifyKind(model.PreferredDate.Date, DateTimeKind.Utc),
                BookingTime = model.PreferredTime,
                GuestCount = model.GuestCount,
                SpecialRequest = model.SpecialRequest,
                TotalPrice = totalPrice,
                BookingStatus = "pending"
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Go to the confirmation page
            return RedirectToAction("Confirmation", new { id = booking.BookingId });
        }


        // GET: /BookingsFE/Confirmation/5

        public async Task<IActionResult> Confirmation(int id)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var booking = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BookingId == id && b.UserId == userId.Value);

            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            return View(booking); // Views/BookingsFE/Confirmation.cshtml
        }



        //This is the single place to pull all bookings for the logged-in user.
        public async Task<IActionResult> MyBookings()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var bookings = await _context.Bookings
                .Include(b => b.Event)
                .Where(b => b.UserId == userId.Value &&
                            b.BookingStatus != "cancelled") // simple “active” filter
                .OrderBy(b => b.BookingDate)
                .ThenBy(b => b.BookingTime)
                .ToListAsync();

            return View(bookings); // Views/BookingsFE/MyBookings.cshtml
        }

        // GET: /BookingsFE/TicketQr/5
        [HttpGet]
        public async Task<IActionResult> TicketQr(int id)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            // Ensure the booking belongs to the logged-in user
            var booking = await _context.Bookings
                .Include(b => b.Event)
                .FirstOrDefaultAsync(b => b.BookingId == id && b.UserId == userId.Value);

            if (booking == null)
            {
                return NotFound();
            }

            // The URL we want encoded in the QR code – use your confirmation page
            var ticketUrl = Url.Action(
                action: "Confirmation",
                controller: "BookingsFE",
                values: new { id = booking.BookingId },
                protocol: Request.Scheme);

            // Generate QR code as PNG bytes
            using var qrGenerator = new QRCodeGenerator();
            using QRCodeData qrCodeData = qrGenerator.CreateQrCode(ticketUrl, QRCodeGenerator.ECCLevel.Q);

            // PngByteQRCode is available in QRCoder 1.7+
            using var qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrBytes = qrCode.GetGraphic(20); // 20 = pixels per module

            return File(qrBytes, "image/png");
        }

    }
}
