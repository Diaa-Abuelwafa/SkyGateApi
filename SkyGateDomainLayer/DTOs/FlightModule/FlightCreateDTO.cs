using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.DTOs.FlightModule
{
    public class FlightCreateDTO
    {
        public int FlightNumber { get; set; }
        public string CompanyName { get; set; }
        public string DepartureAirportName { get; set; }
        public string ArrivalAirportName { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public int AirplaneId { get; set; }
        public decimal AdultPrice { get; set; }
        public decimal ChildPrice { get; set; }
    }
}
