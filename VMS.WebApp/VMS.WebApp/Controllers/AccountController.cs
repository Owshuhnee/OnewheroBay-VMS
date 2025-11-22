using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VMS.WebApp.Data;
using VMS.WebApp.Models;

namespace VMS.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Account()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId.Value);

            if (user == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var bookings = await _context.Bookings
                .Where(b => b.UserId == userId.Value)
                .Include(b => b.Event)
                .OrderByDescending(b => b.BookingDate)
                .Select(b => new BookingSummaryViewModel
                {
                    BookingId = b.BookingId,
                    EventName = b.Event.EventName,
                    BookingDate = b.BookingDate,
                    BookingTime = b.BookingTime,
                    TotalPrice = b.TotalPrice,
                    BookingStatus = b.BookingStatus
                })
                .ToListAsync();

            var vm = new AccountViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone ?? "",
                Bookings = bookings
            };

            return View(vm);   // looks for Views/Account/Account.cshtml
        }
    }
}
