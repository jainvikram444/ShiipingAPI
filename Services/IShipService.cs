using Microsoft.AspNetCore.Mvc;
using ShiipingAPI.Models;
using ShiipingAPI.RespnseModels;

namespace ShiipingAPI.Services
{
    public interface IShipService
    {
        public IEnumerable<Ship> GetShipList();
        public ShipPortResponse GetShipById(int id);
        public Ship AddShip(Ship Ship);
        public Ship UpdateShip(int Id, Ship Ship);
        public bool DeleteShip(int Id);
    }
}
