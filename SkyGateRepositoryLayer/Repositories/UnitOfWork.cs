using SkyGateDomainLayer.Interfaces.AirplaneModule;
using SkyGateDomainLayer.Interfaces.UnitOfWork;
using SkyGateRepositoryLayer.Data.Contexts;
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
