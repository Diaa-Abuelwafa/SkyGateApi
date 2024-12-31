using AutoMapper;
using SkyGateDomainLayer.DTOs.FlightModule;
using SkyGateDomainLayer.Entities.FlightModule;
using SkyGateDomainLayer.Interfaces.FlightModule;
using SkyGateDomainLayer.Interfaces.Specifications;
using SkyGateDomainLayer.Interfaces.UnitOfWork;
using SkyGateRepositoryLayer.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateServiceLayer.Services.FlightRepository
{
    public class FlightService : IFlightService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;

        public FlightService(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            this.UnitOfWork = UnitOfWork;
            this.Mapper = Mapper;
        }

        public int AddFlight(FlightCreateDTO Flight)
        {
            var FlightDto = Mapper.Map<Flight>(Flight);

            UnitOfWork.FlightRepository().Add(FlightDto);

            return UnitOfWork.SaveChanges();
        }

        public int DeleteFlight(int FlightId)
        {
            UnitOfWork.FlightRepository().Delete(FlightId);

            return UnitOfWork.SaveChanges();
        }

        public List<FlightDTO> GetAllFlights()
        {
            var FlightsFromDB = UnitOfWork.FlightRepository().GetAllWithoutSpec();

            var Flights = Mapper.Map<List<FlightDTO>>(FlightsFromDB);

            if (Flights is not null)
            {
                return Flights;
            }

            return null;
        }

        public List<FlightDTO> GetAllFlightsWithSpec(FlightParams Params)
        {
            var Spec = new Specification<Flight, int>();

            Spec.Criteria = P => (P.DepartureDateTime > DateTime.Now) && P.DepartureDateTime.Date == Params.DepartureDate.Date ||
                            P.DepartureDateTime.Date == Params.ReturnDate.Date ||
                            (P.DepartureAirportName == Params.From && P.ArrivalAirportName == Params.To) ||
                            (P.DepartureAirportName == Params.To && P.ArrivalAirportName == Params.From);


            Spec.Includes.Add(P => P.Airplane);

            var FlightsFromDB = UnitOfWork.FlightRepository().GetAll(Spec);

            var Flights = Mapper.Map<List<FlightDTO>>(FlightsFromDB);

            if (Flights is not null)
            {
                return Flights;
            }
            else
            {
                return null;
            }
        }

        public FlightDTO GetFlightById(int FlightId)
        {
            var Spec = new Specification<Flight, int>();
            Spec.Criteria = p => (p.DepartureDateTime > DateTime.Now) && p.Id == FlightId;
            Spec.Includes.Add(P => P.Airplane);

            var FlightFromDB = UnitOfWork.FlightRepository().GetById(Spec);

            var Flight = Mapper.Map<FlightDTO>(FlightFromDB);

            if(Flight is not null)
            {
                return Flight;
            }

            return null;
        }

        public int UpdateFlight(int Id, FlightCreateDTO Flight)
        {
            var FlightDto = Mapper.Map<Flight>(Flight);

            UnitOfWork.FlightRepository().Update(Id, FlightDto);

            return UnitOfWork.SaveChanges();
        }
    }
}
