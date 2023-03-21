using ShiipingAPI.Models;
using ShiipingAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ShiipingAPI.RespnseModels;
using Microsoft.Data.SqlClient;

namespace ShiipingAPI.Services
{
    public class ShipService : IShipService
    {
        private readonly ShiipingAPIContext _context;

        public ShipService(ShiipingAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ship>> GetShipList()
        {
            var ships = await _context.Ship.Where(r => r.Status.Equals(1)).OrderBy(s => s.Name).Take(100).ToListAsync();
            return ships;
         }

        public async Task<ShipPortResponse> GetShipById(int Id)
        {
            var shipPortResponse = new ShipPortResponse();

            var ship =  _context.Ship.FindAsync(Id);

            if (ship.Result == null)
            {
                return shipPortResponse;
            }

            //Prevent from SQL injetion
            var param = new SqlParameter("@ShipId", Id);
            var portNearByShip = _context.PortNearByShip.FromSqlRaw($"GetPortNearByShip @ShipId", param).ToListAsync();

            shipPortResponse.Id = ship.Result.Id;
            shipPortResponse.Name = ship.Result.Name;
            shipPortResponse.Description = ship.Result.Description;
            shipPortResponse.Latitude = ship.Result.Latitude;
            shipPortResponse.Longitude = ship.Result.Longitude;
            shipPortResponse.Velocity = ship.Result.Velocity;

            if (portNearByShip.Result != null && portNearByShip.Result.Count > 0)
            {
                shipPortResponse.Distance = portNearByShip.Result[0].Distance;
                shipPortResponse.Port = portNearByShip.Result[0].PortName;
                var totalHour = portNearByShip.Result[0].Distance / ship.Result.Velocity;
                var arrivalTime = DateTime.Now.ToUniversalTime();
                arrivalTime.AddHours(totalHour);
                shipPortResponse.ArrivalTime = arrivalTime.ToShortDateString();
            }

            return shipPortResponse;
        }

        public async Task<Ship> AddShip(ShipRequest shipRequest)
        {            
            var ship = new Ship();
            ship.Name = shipRequest.Name;
            ship.Description = shipRequest.Description;
            ship.Latitude = shipRequest.Latitude;
            ship.Longitude = shipRequest.Longitude;
            ship.Velocity = shipRequest.Velocity;
            ship.Status = 1;
           
            var result = await _context.Ship.AddAsync(ship);

            try
            {
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
         }

        public async Task<Ship> UpdateShip(int Id, ShipRequest shipRequest)
        {
            if (Id <= 0)
            {
                return null;
            }
                      
            var _ship = await _context.Ship.FirstOrDefaultAsync(e => e.Id == Id);

            if (_ship == null)
            {
                return null; ;
            }
            _ship.Velocity = shipRequest.Velocity;
            _ship.Latitude = shipRequest.Latitude == 0 ? _ship.Latitude : shipRequest.Latitude;
            _ship.Longitude = shipRequest.Longitude == 0 ? _ship.Longitude : shipRequest.Longitude;
            _ship.Name = string.IsNullOrEmpty(shipRequest.Name) ? _ship.Name : shipRequest.Name;
            _ship.Description = string.IsNullOrEmpty(shipRequest.Description) ? _ship.Description : shipRequest.Description;

            var result =  _context.Ship.Update(_ship);
            try
            {
               await _context.SaveChangesAsync();
               return result.Entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Ship> UpdateShipVelocity(int Id, int Velocity)
        {
            if (Id <= 0)
            {
                return null;
            } 

            var _ship = await _context.Ship.FirstOrDefaultAsync(e => e.Id == Id);

            if (_ship == null)
            {
                return null; ;
            }
            _ship.Velocity = Velocity;
           
            var result = _context.Ship.Update(_ship);
            try
            {
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteShip(int Id)
        {
            var ship = await _context.Ship.FindAsync(Id);
            if (ship == null)
            {
                return false;
            }

            _context.Ship.Remove(ship);
            var result = await _context.SaveChangesAsync();

            return result > 0 ? true : false;
        }

        private bool ShipExists(int id)
        {
            return _context.Ship.Any(e => e.Id == id);
        }
    }
}
