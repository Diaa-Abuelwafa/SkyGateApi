using SkyGateDomainLayer.Entities.AirplaneModule;
using SkyGateDomainLayer.Entities.BaseEntity;
using SkyGateDomainLayer.Entities.ReservationModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Entities.FlightModule
{
    public class Flight : BaseEntity<int>
    {
        public int FlightNumber { get; set; }
        public string CompanyName { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateOnly DepartureDate { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public TimeOnly DepartureTime { get; set; }
        public TimeOnly ArrivalTime { get; set; }
        public Airplane Airplane { get; set; }
        public int AirplaneId { get; set; }
        public IEnumerable<int> EconomyToken { get; set; }
        public IEnumerable<int> BusinessToken { get; set; }
        public IEnumerable<int> FirstClassToken { get; set; }
        public decimal AdultPrice { get; set; }
        public decimal ChildPrice { get; set; }
        public IQueryable<Reservation> Reservations { get; set; }

        public TimeSpan Duration()
        {
            // Duration.Hours & Duration.Minutes
            return ArrivalTime - DepartureTime;
        }

        // TODO
        public bool IsFull()
        {
            return true;
        }
    }
}
