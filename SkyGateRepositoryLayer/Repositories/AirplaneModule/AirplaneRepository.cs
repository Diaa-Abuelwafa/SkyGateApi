using SkyGateDomainLayer.Entities.AirplaneModule;
using SkyGateDomainLayer.Interfaces.AirplaneModule;
using SkyGateRepositoryLayer.Data.Contexts;
using SkyGateRepositoryLayer.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateRepositoryLayer.Repositories.AirplaneModule
{
    public class AirplaneRepository : GenericRepository<Airplane, int> , IAirplaneRepository
    {
        private readonly AppDbContext Context;

        public AirplaneRepository(AppDbContext Context) : base(Context)
        {
            this.Context = Context;
        }

        public ICollection<Airplane> GetFree()
        {
            
        }
    }
}
