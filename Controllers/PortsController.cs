using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiipingAPI.Data;
using ShiipingAPI.Models;
using ShiipingAPI.Services;

namespace ShiipingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class PortsController : ControllerBase
    {
        private readonly IPortService portService;
        public PortsController(IPortService _portService)
        {
            portService = _portService;
        }

        // GET: api/Ports
        [HttpGet]
        public IEnumerable<Port> GetPortList()
        {
            var portList =  portService.GetPortList();
            return portList;
        }

        // GET: api/Ports/5
        [HttpGet("{id}")]
        public Port GetPortById(int id)
        {
            return portService.GetPortById(id);
        }

        // PUT: api/Ports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public Port PutPort(int id, Port port)
        {
            return portService.UpdatePort(id, port);
        }

        // POST: api/Ports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Port PostPort(Port port)
        {
            return portService.AddPort(port);
        }

        // DELETE: api/Ports/5
        [HttpDelete("{id}")]
        public bool DeletePort(int id)
        {
            return portService.DeletePort(id);            
        }
 
    }
}
