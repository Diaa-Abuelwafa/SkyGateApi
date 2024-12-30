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
        public string DepartureAirportName { get; set; }
        public string ArrivalAirportName { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public Airplane Airplane { get; set; }
        public int AirplaneId { get; set; }
        public List<int> EconomyToken { get; set; } = new List<int>();
        public List<int> BusinessToken { get; set; } = new List<int>();
        public List<int> FirstClassToken { get; set; } = new List<int>();
        public decimal AdultPrice { get; set; }
        public decimal ChildPrice { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        public TimeSpan Duration()
        {
            // TODO
            return TimeSpan.MaxValue;
        }

        // TODO
        public bool IsFull()
        {
            return true;
        }
    }
}
