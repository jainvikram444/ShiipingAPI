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

        public DbSet<ShiipingAPI.Models.Port> Port { get; set; }

        //Ignore for the database migration
        public DbSet<ShiipingAPI.Models_KeyLess.PortNearByShip> PortNearByShip { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ShiipingAPI.Models_KeyLess.PortNearByShip>().HasNoKey();
            modelBuilder.Entity<ShiipingAPI.Models_KeyLess.PortNearByShip>().ToTable(nameof(ShiipingAPI.Models_KeyLess.PortNearByShip), t => t.ExcludeFromMigrations());
        }

    }
}
