namespace ShiipingAPI.RespnseModels
{
    public class ShipRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Velocity { get; set; }
    }
}
