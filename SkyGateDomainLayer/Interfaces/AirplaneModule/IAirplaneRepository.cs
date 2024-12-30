using SkyGateDomainLayer.Entities.AirplaneModule;
using SkyGateDomainLayer.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Interfaces.AirplaneModule
{
    public interface IAirplaneRepository : IGenericRepository<Airplane, int>
    {
        public ICollection<Airplane> GetFree();
    }
}
