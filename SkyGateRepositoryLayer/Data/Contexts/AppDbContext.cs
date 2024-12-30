using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkyGateDomainLayer.Entities.AirplaneModule;
using SkyGateDomainLayer.Entities.FlightModule;
using SkyGateDomainLayer.Entities.Identity;
using SkyGateDomainLayer.Entities.ReservationModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateRepositoryLayer.Data.Contexts
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public AppDbContext(DbContextOptions Options) : base(Options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
