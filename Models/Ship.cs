using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShiipingAPI.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Ship
    {
         public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength =3)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Display(Name = "Location Longitude")]
        public double Longitude { get; set; }

        [Display(Name = "Velocity")]
        [Range(0,9999)]
        public int Velocity { get; set; }

        [Required]
        [JsonIgnore]
        public Int16 Status { get; set; } = 1; //1: Active, 2:Inactive, 3: Deleted

        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
