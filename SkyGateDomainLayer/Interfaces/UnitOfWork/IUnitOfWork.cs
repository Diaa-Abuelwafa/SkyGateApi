using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        public int SaveChanges();
    }
}
