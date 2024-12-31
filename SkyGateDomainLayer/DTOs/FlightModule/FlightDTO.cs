using SkyGateDomainLayer.Entities.AirplaneModule;
using SkyGateDomainLayer.Entities.ReservationModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.DTOs.FlightModule
{
    public class FlightDTO
    {
        public int FlightNumber { get; set; }
        public string CompanyName { get; set; }
        public string DepartureAirportName { get; set; }
        public string ArrivalAirportName { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public int AirplaneId { get; set; }
        public List<int> EconomyToken { get; set; } = new List<int>();
        public List<int> BusinessToken { get; set; } = new List<int>();
        public List<int> FirstClassToken { get; set; } = new List<int>();
        public decimal AdultPrice { get; set; }
        public decimal ChildPrice { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
