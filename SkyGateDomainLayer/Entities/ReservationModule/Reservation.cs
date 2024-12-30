using SkyGateDomainLayer.Entities.BaseEntity;
using SkyGateDomainLayer.Entities.FlightModule;
using SkyGateDomainLayer.Entities.Identity;
using SkyGateDomainLayer.Enums.PaymentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Entities.ReservationModule
{
    public class Reservation : BaseEntity<int>
    {
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public IQueryable<Flight> Flights { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChilds { get; set; }
        public string CabinName { get; set; }
        public IEnumerable<int> SeatsToken { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        // TODO
        public decimal TotalPrice()
        {
            return 0;
        }

    }
}
