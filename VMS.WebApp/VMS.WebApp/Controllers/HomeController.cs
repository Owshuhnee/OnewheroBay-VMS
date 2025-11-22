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

        public IActionResult Account()
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

        public IActionResult Events()
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
                // Reload the login/register page if validation fails
                return View("LoginRegister", model);   // use "Index" if that's where your form is
            }

            // Optional: prevent duplicate email
            var emailExists = await _context.Users
                .AnyAsync(u => u.Email == model.Email);

            if (emailExists)
            {
                ModelState.AddModelError("", "Email is already registered.");
                return View("LoginRegister", model);   // or "Index"
            }

            var user = new User
            {
                Role = "Visitor",               // default role
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Email = model.Email,
                Password = model.Password,      // TODO: hash later
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Auto-login after successful registration
            HttpContext.Session.SetString("UserFirstName", user.FirstName);

            // Optional message (if you still want it)
            TempData["Registered"] = "Account created successfully.";

            // Send them to home so navbar can now show "Welcome, FirstName!"
            return RedirectToAction("Index", "Home");
        }

        // LOGIN ACTION
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View("LoginRegister", model);   // or "Index"

            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Email == model.Email &&
                    u.Password == model.Password);     // TODO: hash compare later

            if (user == null)
            {
                TempData["Error"] = "Invalid email or password.";
                // back to login/register page
                return RedirectToAction("LoginRegister");  // or RedirectToAction("Index")
            }

            // Store first name in session (navbar uses this)
            HttpContext.Session.SetString("UserFirstName", user.FirstName);

            // Redirect to homepage (Account button will now say Welcome, FirstName!)
            return RedirectToAction("Index", "Home");
        }
    }
}