using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShiipingAPI.Data;
using ShiipingAPI.Models;
using ShiipingAPI.RespnseModels;
using ShiipingAPI.Services;

namespace ShiipingAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipsController : ControllerBase
    {
        private readonly IShipService shipService;
        public ShipsController(IShipService _shipService)
        {
            shipService = _shipService;
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
        public IEnumerable<Ship> GetShip()
        {
            var shiptList = shipService.GetShipList();
            return shiptList;
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
        public ShipPortResponse GetShip(int id)
        {
            return shipService.GetShipById(id);
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
        public  Ship PutShip(int id, Ship ship)
        {
            return shipService.UpdateShip(id, ship);

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
        public Ship PostShip( Ship ship)
        {
            return shipService.AddShip(ship);
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
        public bool DeleteShip(int id)
        {
            return shipService.DeleteShip(id);

        }   
    }
}
