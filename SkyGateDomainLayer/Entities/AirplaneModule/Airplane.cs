using SkyGateDomainLayer.Entities.BaseEntity;
using SkyGateDomainLayer.Entities.FlightModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Entities.AirplaneModule
{
    public class Airplane : BaseEntity<int>
    {
        public string Model { get; set; }
        public int NumberOfEconomySeats { get; set; }
        public int NumberOfBusinessSeats { get; set; }
        public int NumberOfFirstClassSeats { get; set; }
        public decimal AdultBaggage { get; set; }
        public decimal ChildBaggage { get; set; }
        public IQueryable<Flight> Flights { get; set; }
    }
}
