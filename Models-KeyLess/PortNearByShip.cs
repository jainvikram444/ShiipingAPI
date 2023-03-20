using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShiipingAPI.Models_KeyLess
{
    [Keyless]
    public class PortNearByShip
    {
        public int Distance { get; set; }
        public string PortName { get; set; }
        public int PortId { get; set; }
    }
}
