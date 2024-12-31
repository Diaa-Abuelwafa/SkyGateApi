using SkyGateDomainLayer.Interfaces.AirplaneModule;
using SkyGateDomainLayer.Interfaces.FlightModule;
using SkyGateDomainLayer.Interfaces.ReservationModule;
using SkyGateDomainLayer.Interfaces.UnitOfWork;
using SkyGateRepositoryLayer.Data.Contexts;
using SkyGateRepositoryLayer.Repositories.AirplaneModule;
using SkyGateRepositoryLayer.Repositories.FlightModule;
using SkyGateRepositoryLayer.Repositories.ReservationModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateRepositoryLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext Context;
        private IAirplaneRepository _AirplaneRepository;
        private IFlightRepository _FlightRepository;
        private IReservationRepository _IReservationRepository;
        public UnitOfWork(AppDbContext Context)
        {
            this.Context = Context;
        }

        public IAirplaneRepository AirplaneRepository()
        {
            if(_AirplaneRepository is null)
            {
                _AirplaneRepository = new AirplaneRepository(Context);
            }

            return _AirplaneRepository;
        }

        public IFlightRepository FlightRepository()
        {
            if(_FlightRepository is null)
            {
                _FlightRepository = new FlightRepository(Context);
            }

            return _FlightRepository;
        }

        public IReservationRepository ReservationRepository()
        {
            if (_IReservationRepository is null)
            {
                _IReservationRepository = new ReservationRepository(Context);
            }

            return _IReservationRepository;
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

    }
}
