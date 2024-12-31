using SkyGateDomainLayer.Entities.FlightModule;
using SkyGateDomainLayer.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Interfaces.FlightModule
{
    public interface IFlightRepository : IGenericRepository<Flight, int>
    {
        public List<Flight> GetAllWithoutSpec();
    }
}
