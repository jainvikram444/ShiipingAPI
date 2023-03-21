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
        /// <response code="404">If record not found </response>
        /// <response code="400">If something wrong in process</response>
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
        /// <response code="200">Returns the Ship details</response>
        /// <response code="404">If record not found </response>
        /// <response code="400">If something wrong in process</response>    
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
        /// <param name="Id"></param>
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
        /// <response code="200">Returns the updated details of Ship</response>
        /// <response code="404">If record not found </response>
        /// <response code="400">If something wrong in process</response>
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(Response<Ship>))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(SwaggerErrorResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(SwaggerErrorResponse))]
        [Produces("application/json")]
        [HttpPut("{Id}")]
        public async Task<ActionResult<Response<Ship>>> PutShip(int Id, ShipRequest shipRequest)
        {
            var shipResponse = await shipService.UpdateShip(Id, shipRequest);
            if (shipResponse != null && shipResponse.Id > 0)
            {
                IEnumerable<Ship> shipResponses = new[] { shipResponse };
                var sussessResponse = new Response<Ship>(true, "Successfully update the record", shipResponses);
                return Ok(sussessResponse);
            }
            else if (shipResponse != null && shipResponse.Id == 0)
            {
                var sussessResponse = new Response<Ship>(false, $"Record not update for the ID: {Id}", null);
                return NotFound(sussessResponse);
            }

            var errorResponse = new Response<Ship>(false, "Something wrong in server. Pleae try agin.", null);
            return BadRequest(errorResponse);
        }

        // POST: api/Ships/5/UpdateVelocity/35
        /// <summary>
        /// Update velocity of Ship
        /// </summary>        
        /// <param name="Id"></param>
        /// <returns>Details of Ship</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Ships/5/UpdateVelocity/30
        ///
        /// </remarks>
        /// <response code="200">Returns the updated details of Ship</response>
        /// <response code="404">If record not found </response>
        /// <response code="400">If something wrong in process</response>
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(Response<Ship>))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(SwaggerErrorResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(SwaggerErrorResponse))]
        [Produces("application/json")]
        [HttpPost("{Id}/UpdateVelocity/{Velocity}")]
        public async Task<ActionResult<Response<Ship>>> PostUpdateShipVelocity(int Id, int Velocity)
        {
            var shipResponse = await shipService.UpdateShipVelocity(Id, Velocity);
            if (shipResponse != null && shipResponse.Id > 0)
            {
                IEnumerable<Ship> shipResponses = new[] { shipResponse };
                var sussessResponse = new Response<Ship>(true, "Successfully update the velocity of Ship", shipResponses);
                return Ok(sussessResponse);
            }
            else if (shipResponse != null && shipResponse.Id == 0)
            {
                var sussessResponse = new Response<Ship>(false, $"Record not update for the ID: {Id}", null);
                return NotFound(sussessResponse);
            }

            var errorResponse = new Response<Ship>(false, "Something wrong in server. Pleae try agin.", null);
            return BadRequest(errorResponse);
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
        /// <response code="200">Returns the inserted record of Ship</response>
        /// <response code="404">If record not inserted due some resBtriction</response>
        /// <response code="400">If something wrong in process</response>
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(Response<Ship>))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(SwaggerErrorResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(SwaggerErrorResponse))]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<Response<Ship>>> PostShip(ShipRequest shipRequest)
        {
            var shipResponse = await shipService.AddShip(shipRequest);
            if (shipResponse != null && shipResponse.Id > 0)
            {
                IEnumerable<Ship> shipResponses = new[] { shipResponse };
                var sussessResponse = new Response<Ship>(true, "Successfully insert the record", shipResponses);
                return Ok(sussessResponse);
            }
            else if (shipResponse != null && shipResponse.Id == 0)
            {
                var sussessResponse = new Response<Ship>(false, $"Failed to insert the record.", null);
                return NotFound(sussessResponse);
            }

            var errorResponse = new Response<Ship>(false, "Something wrong in server. Pleae try agin.", null);
            return BadRequest(errorResponse);
        }

        // DELETE: api/Ships/5
        /// <summary>
        /// Delete Ship.
        /// </summary>        
        /// <param name="Id"></param>
        /// <returns>Delete Ship</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/Ships/5
        ///
        /// </remarks>
        /// <response code="200">Returns the inserted record of Ship</response>
        /// <response code="404">If record not inserted due some resBtriction</response>
        /// <response code="400">If something wrong in process</response>
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(SwaggerBoolResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(SwaggerErrorResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(SwaggerErrorResponse))]
        [Produces("application/json")]
        [HttpDelete("{Id}")]
        public async Task<ActionResult<Response<Ship>>> DeleteShip(int Id)
        {
            var shipDeleteResponse = await shipService.DeleteShip(Id);
            if (shipDeleteResponse)
            {
                var sussessResponse = new Response<Ship>(true, "Successfully delete the record", null);
                return Ok(sussessResponse);
            }
            else if (!shipDeleteResponse)
            {
                var sussessResponse = new Response<Ship>(false, $"Failed to delete the record.", null);
                return NotFound(sussessResponse);
            }

            var errorResponse = new Response<Ship>(false, "Something wrong in server. Pleae try agin.", null);
            return BadRequest(errorResponse);
        }   
    }
}
