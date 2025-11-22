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

        public IActionResult Promotions()
        {
            return View();
        }
        public IActionResult Booking()
        {
            return View();
        }
        public IActionResult Login()
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
                // If form fields are missing or invalid, reload the same page for now
                return View("Index", model);
            }

            var user = new User
            {
                Role = "Visitor",               // default role
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Email = model.Email,
                Password = model.Password,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            TempData["Registered"] = "Account created successfully. Please log in.";
            return RedirectToAction("Index");
        }

        // LOGIN ACTION
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
            {
                TempData["Error"] = "Invalid email or password.";
                return RedirectToAction("Index");
            }

            // Store first name in session
            HttpContext.Session.SetString("UserFirstName", user.FirstName);

            // Redirect to homepage
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Analytics()
        {
            return View();
        }
    }
}
