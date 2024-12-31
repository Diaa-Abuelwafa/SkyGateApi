using SkyGateDomainLayer.Interfaces.AirplaneModule;
using SkyGateDomainLayer.Interfaces.FlightModule;
using SkyGateDomainLayer.Interfaces.ReservationModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IAirplaneRepository AirplaneRepository();
        public IFlightRepository FlightRepository();
        public IReservationRepository ReservationRepository();
        public int SaveChanges();
    }
}
