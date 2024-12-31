using Microsoft.EntityFrameworkCore;
using SkyGateDomainLayer.Entities.FlightModule;
using SkyGateDomainLayer.Interfaces.FlightModule;
using SkyGateRepositoryLayer.Data.Contexts;
using SkyGateRepositoryLayer.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateRepositoryLayer.Repositories.FlightModule
{
    public class FlightRepository : GenericRepository<Flight, int>, IFlightRepository
    {
        private readonly AppDbContext Context;

        public FlightRepository(AppDbContext Context) : base(Context)
        {
            this.Context = Context;
        }

        public List<Flight> GetAllWithoutSpec()
        {
            return Context.Flights.Include(x => x.Airplane).Include(x => x.Reservations).Where(x => x.DepartureDateTime > DateTime.Now).ToList();
        }
    }
}
