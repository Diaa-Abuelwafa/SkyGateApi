using SkyGateDomainLayer.Interfaces.AirplaneModule;
using SkyGateDomainLayer.Interfaces.UnitOfWork;
using SkyGateRepositoryLayer.Data.Contexts;
using SkyGateRepositoryLayer.Repositories.AirplaneModule;
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
        private AirplaneRepository _AirplaneRepository;
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

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

    }
}
