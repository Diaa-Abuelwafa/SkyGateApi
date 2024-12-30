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

        private IAirplaneRepository _AirplaneRepository;

        public IAirplaneRepository AirplaneRepository
        {
            get
            {
                if(_AirplaneRepository is null)
                {
                    _AirplaneRepository = new AirplaneRepository(Context);
                }

                return _AirplaneRepository;
            }
        }
        public UnitOfWork(AppDbContext Context)
        {
            this.Context = Context;
        }
        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
