using Microsoft.AspNetCore.Mvc;
using ShiipingAPI.Models;
using ShiipingAPI.RespnseModels;

namespace ShiipingAPI.Services
{
    public interface IShipService
    {
        public Task<IEnumerable<Ship>> GetShipList();
        public Task<ShipPortResponse> GetShipById(int id);
        public Ship AddShip(Ship Ship);
        public Ship UpdateShip(int Id, Ship Ship);
        public bool DeleteShip(int Id);
    }
}
