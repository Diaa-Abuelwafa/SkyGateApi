using SkyGateDomainLayer.Entities.ReservationModule;
using SkyGateDomainLayer.Interfaces.ReservationModule;
using SkyGateRepositoryLayer.Data.Contexts;
using SkyGateRepositoryLayer.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateRepositoryLayer.Repositories.ReservationModule
{
    public class ReservationRepository : GenericRepository<Reservation, int> , IReservationRepository
    {
        public ReservationRepository(AppDbContext Context) : base(Context)
        {

        }
    }
}
