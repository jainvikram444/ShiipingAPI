using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiipingAPI.Data;
using ShiipingAPI.Models;

namespace ShiipingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class PortsController : ControllerBase
    {
        private readonly ShiipingAPIContext _context;

        public PortsController(ShiipingAPIContext context)
        {
            _context = context;
        }

        // GET: api/Ports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Port>>> GetPort()
        {
            return await _context.Port.ToListAsync();
        }

        // GET: api/Ports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Port>> GetPort(int id)
        {
            var port = await _context.Port.FindAsync(id);

            if (port == null)
            {
                return NotFound();
            }

            return port;
        }

        // PUT: api/Ports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPort(int id, Port port)
        {
            if (id != port.Id)
            {
                return BadRequest();
            }

            _context.Entry(port).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Ports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Port>> PostPort(Port port)
        {
            _context.Port.Add(port);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPort", new { id = port.Id }, port);
        }

        // DELETE: api/Ports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePort(int id)
        {
            var port = await _context.Port.FindAsync(id);
            if (port == null)
            {
                return NotFound();
            }

            _context.Port.Remove(port);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PortExists(int id)
        {
            return _context.Port.Any(e => e.Id == id);
        }
    }
}
