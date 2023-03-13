using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShiipingAPI.Models;

namespace ShiipingAPI.Data
{
    public class ShiipingAPIContext : DbContext
    {
        public ShiipingAPIContext (DbContextOptions<ShiipingAPIContext> options)
            : base(options)
        {
        }

        public DbSet<ShiipingAPI.Models.Ship> Ship { get; set; } = default!;
    }
}
