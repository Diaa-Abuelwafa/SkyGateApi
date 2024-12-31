using SkyGateDomainLayer.DTOs.FlightModule;
using SkyGateDomainLayer.Entities.FlightModule;
using SkyGateDomainLayer.Interfaces.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Interfaces.FlightModule
{
    public interface IFlightService
    {
        public List<FlightDTO> GetAllFlights();
        public List<FlightDTO> GetAllFlightsWithSpec(FlightParams Params);
        public FlightDTO GetFlightById(int FlightId);
        public int AddFlight(FlightCreateDTO Flight);
        public int UpdateFlight(int Id, FlightCreateDTO Flight);
        public int DeleteFlight(int FlightId);
    }
}
