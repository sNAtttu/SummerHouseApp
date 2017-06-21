using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SummerHouseApplication.Models;
using SummerHouseApplication.Models.Map;

namespace SummerHouseApplication.Data
{
    public class SummerHouseDbContext : IdentityDbContext<SummerHouseUser>
    {
        public SummerHouseDbContext(DbContextOptions<SummerHouseDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<SummerHouse> SummerHouses { get; set; }
        public DbSet<SharedSummerHouse> SharedSummerHouses { get; set; }
        public DbSet<FishingNet> FishingNets { get; set; }
        public DbSet<MapMarker> Markers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<InfoWindow> InfoWindows { get; set; }
    }
}
