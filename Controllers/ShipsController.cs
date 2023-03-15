using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShiipingAPI.Data;
using ShiipingAPI.Models;
using ShiipingAPI.RespnseModels;

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

        /// <summary>
        /// Get Ship List.
        /// </summary>        
        /// <returns>List of Ships</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Ships 
        ///
        /// </remarks>
        /// <response code="200">Returns the list of Ship</response>
        /// <response code="400">If the item is null</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Ship>>> GetShip()
        {
             return await _context.Ship.Where(r => r.Status.Equals(1)).OrderBy(s => s.Name).Take(100).ToListAsync();
        }


        /// <summary>
        /// Get Ship Details.
        /// </summary>        
        /// <param name="id"></param>
        /// <returns>Details of Ship</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Ships/5
        ///
        /// </remarks>
        /// <response code="200">Returns the details of Ship</response>
        /// <response code="400">If the item is null</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ShipPortResponse>> GetShip(int id)
        {
            var shipPortResponse = new ShipPortResponse();

            var ship = await _context.Ship.FindAsync(id);
           
            if (ship == null)
            {
                return NotFound();
            }
        

            var port = await _context.Port.FromSqlRaw($"SELECT top 1  cast(geography::Point(latitude, longitude, 4326).STDistance('POINT({ship.Longitude} {ship.Latitude})')/1000 as int) as distance, port.* FROM dbo.port where status=1 order by distance asc").FirstOrDefaultAsync(); 

            shipPortResponse.Id = ship.Id;
            shipPortResponse.Name = ship.Name;
            shipPortResponse.Description = ship.Description;
            shipPortResponse.Latitude = ship.Latitude;
            shipPortResponse.Longitude = ship.Longitude;
            shipPortResponse.Id = ship.Velocity;

            if (port != null)
            {
                shipPortResponse.Distance = port.Distance;
                shipPortResponse.Port = port.Name;
                var totalHour = port.Distance / ship.Velocity;            
                var arrivalTime = DateTime.Now.ToUniversalTime();
                arrivalTime.AddHours(totalHour);
                shipPortResponse.ArrivalTime = arrivalTime.ToShortDateString();

            }

            return shipPortResponse;
        }

        // PUT: api/Ships/5
        /// <summary>
        /// Update Ship Velocity
        /// </summary>        
        /// <param name="id"></param>
        /// <returns>Details of Ship</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Ships/5
        ///     {
        ///        "velocity": 30
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the details of Ship</response>
        /// <response code="400">If the item is null</response>     
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Ship>> PutShip(int id, Ship ship)
        {
            if (id <= 0 )
            {
                return BadRequest();
            }

            if (ship.Velocity <= 0)
            {
                return BadRequest();
            }

            var entity = _context.Ship.FirstOrDefault(e => e.Id == id);

            if (entity == null)
            {
                return NotFound();
            }
            entity.Velocity = ship.Velocity;
            entity.Latitude = ship.Latitude;
            entity.Longitude = ship.Longitude;
            entity.Name = ship.Name;
            entity.Description = ship.Description;

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();

            }

            return entity;
        }

        // POST: api/Ships
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /// <summary>
        /// Create Ship.
        /// </summary>        
        /// <returns>Details of Ship</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Ships
        ///     {
        ///        "name": "string",
        ///        "description": "string",
        ///        "latitude": 56.2132132,
        ///        "longitude": 56.1654,
        ///        "velocity": 30
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the details of Ship</response>
        /// <response code="400">If the item is null</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
         [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<Ship>> PostShip( Ship ship)
        { 
            _context.Ship.Add(ship);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShip", new { id = ship.Id }, ship);
        }

        // DELETE: api/Ships/5
        /// <summary>
        /// Delete Ship.
        /// </summary>        
        /// <param name="id"></param>
        /// <returns>Delete Ship</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/Ships/5
        ///
        /// </remarks>
        /// <response code="200">Returns blank response</response>
        /// <response code="404">If the item is null</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
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
