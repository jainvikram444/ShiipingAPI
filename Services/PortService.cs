using ShiipingAPI.Models;
using ShiipingAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
 

namespace ShiipingAPI.Services
{
    public class PortService : IPortService
    {
        private readonly ShiipingAPIContext _context;

        public PortService(ShiipingAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Port> GetPortList()
        {
            return _context.Port.ToList();
        }

        public Port GetPortById(int id)
        {
            var port =  _context.Port.Find(id);

            if (port == null)
            {
                return null;
            }

            return port;
        }

        public Port AddPort(Port port)
        {
            var result = _context.Port.Add(port);
             _context.SaveChanges();

            return result.Entity;
        }

        public Port UpdatePort(int id, Port port)
        {
            if (id != port.Id)
            {
                return null;
            }

            var result = _context.Port.Update(port);

            try
            {
                _context.SaveChanges();
                return result.Entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortExists(id))
                {
                    //Record the log for furture analysis
                    return null;
                }
                else
                {
                    //Record the log for furture analysis
                    return null;
                }
            }
        }

        public bool DeletePort(int Id)
        {
            var port = _context.Port.Find(Id);
            if (port == null)
            {
                return false;
            }

            _context.Port.Remove(port);
            var result = _context.SaveChanges();

            return result > 0 ? true : false;
        }

        private bool PortExists(int id)
        {
            return _context.Port.Any(e => e.Id == id);
        }
    }
}
