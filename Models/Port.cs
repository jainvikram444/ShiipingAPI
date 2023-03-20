using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace ShiipingAPI.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Port
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]        
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Required]
        [Display(Name = "Location Longitude")]
        public double Longitude { get; set; }

        [Required]
        [JsonIgnore]
        [DefaultValue(true)]
        public Int16 Status { get; set; } = 1; //1: Active, 2:Inactive, 3: Deleted

        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
