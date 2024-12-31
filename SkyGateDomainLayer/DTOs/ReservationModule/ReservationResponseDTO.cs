using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.DTOs.ReservationModule
{
    public class ReservationResponseDTO
    {
        public string UserId { get; set; }
        public int DepartureFlightId { get; set; }
        public int ReturnFlightId { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChilds { get; set; }
        public string CabinName { get; set; }
        public List<int> SeatsToken { get; set; } = new List<int>();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
