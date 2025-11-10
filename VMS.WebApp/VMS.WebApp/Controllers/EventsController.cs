using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VMS.WebApp.Data;
using VMS.WebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VMS.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var evt = await _context.Events.FindAsync(id);
            if (evt == null)
                return NotFound($"Event with ID {id} not found");
            return Ok(evt);
        }

        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(Event evt)
        {
            _context.Events.Add(evt);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEvent), new { id = evt.EventID }, evt);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event evt)
        {
            if (id != evt.EventID)
                return BadRequest("ID mismatch");

            _context.Entry(evt).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var evt = await _context.Events.FindAsync(id);
            if (evt == null)
                return NotFound($"Event with ID {id} not found");

            _context.Events.Remove(evt);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
