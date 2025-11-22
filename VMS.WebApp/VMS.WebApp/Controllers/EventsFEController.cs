// This controller is for Event feature - Customer/User (Non-Admin)

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using VMS.WebApp.Data;

namespace VMS.WebApp.Controllers
{
    public class EventsFEController : Controller
    {
        private readonly AppDbContext _context;

        public EventsFEController(AppDbContext context)
        {
            _context = context;
        }

        // This will respond to: /EventsFE
        public async Task<IActionResult> Index()
        {
            var eventsList = await _context.Events
                .Where(e => e.IsActive)
                .ToListAsync();

            return View(eventsList);   // looks for Views/EventsFE/Index.cshtml
        }
    }
}
