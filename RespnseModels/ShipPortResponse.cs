using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace ShiipingAPI.RespnseModels
{
    public class ShipPortResponse
    {
        public int Id { get; set; }       
        public string Name { get; set; }       
        public string Description { get; set; }   
        public double Latitude { get; set; }         
        public double Longitude { get; set; }
        public int Velocity { get; set; }
        public int Distance { get; set; }
        public string ArrivalTime { get; set; }
        public string Port { get; set; }
    }

    
}
 


 