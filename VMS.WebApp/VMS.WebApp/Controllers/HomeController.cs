using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VMS.WebApp.Data;
using VMS.WebApp.Models;

namespace VMS.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Booking()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult ParkInfo()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // REGISTER ACTION
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("LoginRegister", model);
            }

            var emailExists = await _context.Users
                .AnyAsync(u => u.Email == model.Email);

            if (emailExists)
            {
                ModelState.AddModelError("", "Email is already registered.");
                return View("LoginRegister", model);
            }

            // make a comma-separated string, e.g. "tours,nature"
            var interestsCsv = (model.SelectedInterests != null && model.SelectedInterests.Any())
                ? string.Join(",", model.SelectedInterests)
                : null;

            var user = new User
            {
                Role = "Visitor",
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Email = model.Email,
                Password = model.Password,  // TODO: hash later
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
                Interests = interestsCsv   
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Auto-login after successful registration
            HttpContext.Session.SetString("UserFirstName", user.FirstName);
            HttpContext.Session.SetInt32("UserID", user.UserId);
            HttpContext.Session.SetString("UserRole", user.Role);

            TempData["Registered"] = "Account created successfully.";
            return RedirectToAction("Index", "Home");
        }


        // LOGIN ACTION

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View("LoginRegister", model);   // or "Login"

            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Email == model.Email &&
                    u.Password == model.Password);     // TODO: hash later

            if (user == null)
            {
                TempData["Error"] = "Invalid email or password.";
                return RedirectToAction("LoginRegister");
            }

            // Store user info in Session
            HttpContext.Session.SetInt32("UserID", user.UserId);
            HttpContext.Session.SetString("UserFirstName", user.FirstName);
            HttpContext.Session.SetString("UserRole", user.Role);

            // Redirect to homepage (Account button will now say Welcome, FirstName!)
            return RedirectToAction("Index", "Home");
        }
            return RedirectToAction("Index", "Home");
        }


    // TICKETS
        public IActionResult Tickets()
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Login", "Home");
            }

            // Redirect to My Bookings page
            return RedirectToAction("MyBookings", "BookingsFE");
        }

    }
}