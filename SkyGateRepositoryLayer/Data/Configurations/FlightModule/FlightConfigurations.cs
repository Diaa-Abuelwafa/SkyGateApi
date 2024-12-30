using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkyGateDomainLayer.Entities.FlightModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateRepositoryLayer.Data.Configurations.FlightModule
{
    public class FlightConfigurations : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            // Relationship With Airplane Entity
            builder.HasOne(x => x.Airplane)
                   .WithMany(x => x.Flights)
                   .HasForeignKey(x => x.AirplaneId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.AdultPrice)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.ChildPrice)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
