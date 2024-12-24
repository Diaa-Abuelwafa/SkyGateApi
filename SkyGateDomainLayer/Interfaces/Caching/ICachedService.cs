using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Interfaces.Caching
{
    public interface ICachedService
    {
        public Task<bool> SetDataAsync(string Key, object Value, TimeSpan Time);
        public Task<string> GetDataAsync(string Key);
    }
}
