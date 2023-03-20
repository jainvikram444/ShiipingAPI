using Microsoft.AspNetCore.Mvc;
using ShiipingAPI.Models;
using ShiipingAPI.RespnseModels;

namespace ShiipingAPI.Services
{
    public interface IShipService
    {
        public Task<IEnumerable<Ship>> GetShipList();
        public Task<ShipPortResponse> GetShipById(int Id);
        public Task<Ship> AddShip(ShipRequest shipRequest);
        public Task<Ship> UpdateShip(int Id, ShipRequest shipRequest);
        public Task<bool> DeleteShip(int Id);
    }
}
