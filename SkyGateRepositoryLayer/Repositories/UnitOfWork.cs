using SkyGateDomainLayer.Interfaces.AirplaneModule;
using SkyGateDomainLayer.Interfaces.FlightModule;
using SkyGateDomainLayer.Interfaces.UnitOfWork;
using SkyGateRepositoryLayer.Data.Contexts;
using SkyGateRepositoryLayer.Repositories.AirplaneModule;
using SkyGateRepositoryLayer.Repositories.FlightModule;
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

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

    }
}
