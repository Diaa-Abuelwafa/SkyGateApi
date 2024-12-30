using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkyGateDomainLayer.Entities.AirplaneModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateRepositoryLayer.Data.Configurations.AirplaneModule
{
    internal class AirplaneConfigurations : IEntityTypeConfiguration<Airplane>
    {
        public void Configure(EntityTypeBuilder<Airplane> builder)
        {
            builder.Property(x => x.AdultBaggage)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.ChildBaggage)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
