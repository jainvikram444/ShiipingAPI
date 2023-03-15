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

        public IEnumerable<Ship> GetShipList()
        {
            return _context.Ship.Where(r => r.Status.Equals(1)).OrderBy(s => s.Name).Take(100).ToList();
        }

        public ShipPortResponse GetShipById(int id)
        {
            var shipPortResponse = new ShipPortResponse();

            var ship =  _context.Ship.Find(id);

            if (ship == null)
            {
                return null;
            }

            var port = _context.Port.FromSqlRaw($"SELECT top 1  cast(geography::Point(latitude, longitude, 4326).STDistance('POINT({ship.Longitude} {ship.Latitude})')/1000 as int) as distance, port.id, port.name, port.description, port.latitude, port.longitude, port.CreatedAt, port.UpdatedAT, port.Status FROM dbo.port where status=1 order by distance asc").FirstOrDefault();

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

        public Ship AddShip(Ship ship)
        {
            var result = _context.Ship.Add(ship);
             _context.SaveChanges();

            return result.Entity;
        }

        public Ship UpdateShip(int id, Ship ship)
        {
            if (id <= 0)
            {
                return null;
            }

            if (ship.Velocity <= 0)
            {
                return null;
            }

            var entity = _context.Ship.FirstOrDefault(e => e.Id == id);

            if (entity == null)
            {
                return null; ;
            }
            entity.Velocity = ship.Velocity;
            entity.Latitude = ship.Latitude == 0 ? entity.Latitude : ship.Latitude;
            entity.Longitude = ship.Longitude == 0 ? entity.Longitude : ship.Longitude;
            entity.Name = string.IsNullOrEmpty(ship.Name) ? entity.Name : ship.Name;
            entity.Description = string.IsNullOrEmpty(ship.Description) ? entity.Description : ship.Description;

            var result = _context.Ship.Update(entity);

            try
            {
              _context.SaveChanges();
                return result.Entity;

            }
            catch (DbUpdateConcurrencyException)
            {
                return null;

            }
        }

        public bool DeleteShip(int Id)
        {
            var Ship = _context.Ship.Find(Id);
            if (Ship == null)
            {
                return false;
            }

            _context.Ship.Remove(Ship);
            var result = _context.SaveChanges();

            return result > 0 ? true : false;
        }

        private bool ShipExists(int id)
        {
            return _context.Ship.Any(e => e.Id == id);
        }
    }
}
