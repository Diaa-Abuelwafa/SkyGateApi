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
    public class Reservation
    {
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
        public Flight DepartureFlight { get; set; }
        public int DepartureFlightId { get; set; }
        public Flight? ReturnFlight { get; set; }
        public int? ReturnFlightId { get; set; }
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
