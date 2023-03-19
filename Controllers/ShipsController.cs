using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using Swashbuckle.AspNetCore.Annotations;

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
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(Response<Ship>))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(SwaggerErrorResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(SwaggerErrorResponse))] 
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Response<Ship>>>> GetShip()
        { 
           var shipsResponse =  await shipService.GetShipList();
           if (shipsResponse != null && shipsResponse.Count() > 0 )
            {
                var sussessResponse = new Response<Ship>(true, "Successfully fetch the records", shipsResponse.ToList());
                return Ok(sussessResponse);
            }
            else if (shipsResponse != null && shipsResponse.Count() == 0)
            {
                var blankResponse = new Response<Ship>(false, "Records not fond", null);
                return NotFound(blankResponse);
            }
            var errorResponse = new Response<Ship>(false, "Something wrong in server. Pleae try agin.", null);
            return BadRequest(errorResponse);
        }


        /// <summary>
        /// Get Ship Details.
        /// </summary>        
        /// <param name="Id"></param>
        /// <returns>Details of Ship</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Ships/5
        ///
        /// </remarks>
        /// <response code="200">Returns the details of Ship</response>
        /// <response code="400">If the item is null</response>
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(Response<ShipPortResponse>))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(SwaggerErrorResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(SwaggerErrorResponse))]
        [Produces("application/json")]
        [HttpGet("{Id}")]
        public async Task<ActionResult<Response<ShipPortResponse>>> GetShip(int Id)
        {
            var shipPortResponse = await shipService.GetShipById(Id);
            if (shipPortResponse != null && shipPortResponse.Id > 0 )
            {
                IEnumerable<ShipPortResponse> shipPortResponses = new[] { shipPortResponse };
                var sussessResponse = new Response<ShipPortResponse>(true, "Successfully fetch the records", shipPortResponses);
                return Ok(sussessResponse);
            } 
            else if (shipPortResponse != null && shipPortResponse.Id  == 0)
            {
                IEnumerable<ShipPortResponse> shipPortResponses = new[] { shipPortResponse };
                var sussessResponse = new Response<ShipPortResponse>(false, $"Record not found for the ID: {Id}", null);
                return NotFound(sussessResponse);
            }
                
            var errorResponse = new Response<ShipPortResponse>(false, "Something wrong in server. Pleae try agin.", null);
            return BadRequest(errorResponse);
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
