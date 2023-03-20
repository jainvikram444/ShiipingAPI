using ShiipingAPI.Models;
using ShiipingAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ShiipingAPI.RespnseModels;

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

        public async Task<ShipPortResponse> GetShipById(int id)
        {
            var shipPortResponse = new ShipPortResponse();

            var ship =  _context.Ship.FindAsync(id);

            if (ship.Result == null)
            {
                return shipPortResponse;
            }
                       
            var port = _context.PortNearByShip
                .FromSqlRaw($"SELECT top 1  cast(geography::Point(latitude, longitude, 4326).STDistance('POINT({ship.Result.Longitude} {ship.Result.Latitude})')/1000 as int) as distance," +
                "port.id as PortId, port.name as PortName FROM dbo.port where status=1 order by distance asc")
                 .FirstOrDefaultAsync();

            shipPortResponse.Id = ship.Result.Id;
            shipPortResponse.Name = ship.Result.Name;
            shipPortResponse.Description = ship.Result.Description;
            shipPortResponse.Latitude = ship.Result.Latitude;
            shipPortResponse.Longitude = ship.Result.Longitude;
            shipPortResponse.Velocity = ship.Result.Velocity;

            if (port.Result != null)
            {
                shipPortResponse.Distance = port.Result.Distance;
                shipPortResponse.Port = port.Result.PortName;
                var totalHour = port.Result.Distance / ship.Result.Velocity;
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

        public async Task<Ship> UpdateShip(int id, ShipRequest shipRequest)
        {
            if (id <= 0)
            {
                return null;
            }

            if (shipRequest.Velocity <= 0)
            {
                return null;
            }

            var _ship = await _context.Ship.FirstOrDefaultAsync(e => e.Id == id);

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
