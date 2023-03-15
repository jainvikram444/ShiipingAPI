using Microsoft.AspNetCore.Mvc;
using ShiipingAPI.Models;

namespace ShiipingAPI.Services
{
    public interface IPortService
    {
        public IEnumerable<Port> GetPortList();
        public Port GetPortById(int id);
        public Port AddPort(Port port);
        public Port UpdatePort(int Id, Port port);
        public bool DeletePort(int Id);
    }
}
