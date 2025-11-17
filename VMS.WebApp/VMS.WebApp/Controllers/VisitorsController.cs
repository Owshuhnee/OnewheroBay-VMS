using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMS.WebApp.Data;
using Microsoft.EntityFrameworkCore;
using VMS.WebApp.Models;

namespace VMS.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : ControllerBase
    {
        // This is a field that holds our database context
        private readonly AppDbContext _context;

        // This is the constructor that runs when the controller is created
        public VisitorsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Visitor>>> GetVisitors()
        {
            // Get all visitors from database
            var visitors = await _context.Visitors.ToListAsync();

            // Return the list with 200 OK status
            return Ok(visitors);
        }

        // GET: api/visitors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Visitor>> GetVisitor(int id)
        {
            // Find visitor with this ID
            var visitor = await _context.Visitors.FindAsync(id);

            // If not found, return 404
            if (visitor == null)
            {
                return NotFound($"Visitor with ID {id} not found");
            }

            // Return the visitor with 200 OK
            return Ok(visitor);
        }

        // POST: api/visitors
        [HttpPost]
        public async Task<ActionResult<Visitor>> CreateVisitor(Visitor visitor)
        {
            // Set registration date to now
            visitor.RegistrationDate = DateTime.UtcNow;

            // Add to database context (not saved yet!)
            _context.Visitors.Add(visitor);

            // Save changes to database
            await _context.SaveChangesAsync();

            // Return 201 Created with location header
            return CreatedAtAction(nameof(GetVisitor), new { id = visitor.VisitorID }, visitor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVisitor(int id, Visitor visitor)
        {
            // Check if ID in URL matches ID in body
            if (id != visitor.VisitorID)
            {
                return BadRequest("ID mismatch");
            }

            // Mark as modified
            _context.Entry(visitor).State = EntityState.Modified;

            try
            {
                // Save changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if visitor still exists
                if (!await VisitorExists(id))
                {
                    return NotFound($"Visitor with ID {id} not found");
                }
                else
                {
                    throw; // Re-throw if it's a different error
                }
            }

            return NoContent(); // 204 No Content (success, no body returned)
        }

        // Helper method to check if visitor exists
        private async Task<bool> VisitorExists(int id)
        {
            return await _context.Visitors.AnyAsync(v => v.VisitorID == id);
        }


        // DELETE: api/visitors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(int id)
        {
            // Find the visitor
            var visitor = await _context.Visitors.FindAsync(id);

            if (visitor == null)
            {
                return NotFound($"Visitor with ID {id} not found");
            }

            // Remove from database
            _context.Visitors.Remove(visitor);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content
        }
    }
}
