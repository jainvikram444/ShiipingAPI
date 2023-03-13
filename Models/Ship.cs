using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiipingAPI.Models
{
    
    public class Ship
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength =3)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Location Latitude")]
        public double LocationLat { get; set; }

        [Required]
        [Display(Name = "Location Longitude")]

        public double LocationLong { get; set; }

        [Required]
        public Int16 Status { get; set; } = 1;


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


    }
}
