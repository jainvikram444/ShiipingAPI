using Microsoft.EntityFrameworkCore;
using ShiipingAPI.Data;
using ShiipingAPI.Models;

namespace ShiipingAPI.Migrations
{
    public static class PortSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ShiipingAPIContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ShiipingAPIContext>>()))
            {
                if (context == null || context.Port == null)
                {
                    throw new ArgumentNullException("Null PortContext");
                }

                // Look for any Port.
                if (context.Port.Any())
                {
                    return;   // DB has been seeded
                }

                context.Port.AddRange(
                    new Port
                    {
                        Name = "Mumbai Port",
                        Description = "Mumbai, India",
                        Latitude = 19.298350,
                        Longitude = 72.870584,
                        Status = 1
                    },

                    new Port
                    {
                        Name = "Singapore Port",
                        Description = "Singapore",
                        Latitude = 1.268087,
                        Longitude = 103.841283,
                        Status = 1
                    },

                    new Port
                    {
                        Name = "South Korea Port",
                        Description = "South Korea",
                        Latitude = 34.274665,
                        Longitude = 126.639220,
                        Status = 1
                    },

                    new Port
                    {
                        Name = "Japan Port",
                        Description = "Japan",
                        Latitude = 35.453730,
                        Longitude = 140.830014,
                        Status = 1
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
