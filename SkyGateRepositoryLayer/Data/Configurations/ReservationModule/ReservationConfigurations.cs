using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkyGateDomainLayer.Entities.ReservationModule;
using SkyGateDomainLayer.Enums.PaymentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateRepositoryLayer.Data.Configurations.ReservationModule
{
    public class ReservationConfigurations : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            // Relationship With ApplicationUser Entity
            builder.HasOne(x => x.User)
                   .WithMany(x => x.Reservations)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


            // Relationship With Flight Entity
            builder.HasMany(x => x.Flights)
                   .WithMany(x => x.Reservations);

            builder.Property(x => x.Status)
                   .HasConversion(x => x.ToString(), x => Enum.Parse<PaymentStatus>(x));
        }
    }
}
