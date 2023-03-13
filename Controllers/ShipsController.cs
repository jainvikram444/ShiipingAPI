using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiipingAPI.Data;
using ShiipingAPI.Models;

namespace ShiipingAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipsController : ControllerBase
    {
        private readonly ShiipingAPIContext _context;

        public ShipsController(ShiipingAPIContext context)
        {
            _context = context;
        }

        // GET: api/Ships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ship>>> GetShip()
        {
            return await _context.Ship.ToListAsync();
        }

        // GET: api/Ships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ship>> GetShip(int id)
        {
            var ship = await _context.Ship.FindAsync(id);

            if (ship == null)
            {
                return NotFound();
            }

            return ship;
        }

        // PUT: api/Ships/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShip(int id, Ship ship)
        {
            if (id != ship.Id)
            {
                return BadRequest();
            }

            _context.Entry(ship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipExists(id))
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

        // POST: api/Ships
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ship>> PostShip(Ship ship)
        {
            _context.Ship.Add(ship);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShip", new { id = ship.Id }, ship);
        }

        // DELETE: api/Ships/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShip(int id)
        {
            var ship = await _context.Ship.FindAsync(id);
            if (ship == null)
            {
                return NotFound();
            }

            _context.Ship.Remove(ship);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShipExists(int id)
        {
            return _context.Ship.Any(e => e.Id == id);
        }
    }
}
