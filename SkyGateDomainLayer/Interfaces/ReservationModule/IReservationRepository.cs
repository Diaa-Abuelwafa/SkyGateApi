using SkyGateDomainLayer.Entities.ReservationModule;
using SkyGateDomainLayer.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Interfaces.ReservationModule
{
    public interface IReservationRepository : IGenericRepository<Reservation, int>
    {

    }
}
