using SkyGateDomainLayer.Interfaces.Caching;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkyGateRepositoryLayer.Repositories.Caching
{
    public class CachedService : ICachedService
    {
        private readonly IDatabase Redis;
        public CachedService(IConnectionMultiplexer RedisConnection)
        {
            Redis = RedisConnection.GetDatabase();
        }
        public async Task<bool> SetDataAsync(string Key, object Value, TimeSpan Time)
        {

            if (Value is not null)
            {
                var SerializeOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var ValueToJson = JsonSerializer.Serialize(Value, SerializeOptions);

                bool Result = await Redis.StringSetAsync(Key, ValueToJson, Time);

                if(Result)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<string> GetDataAsync(string Key)
        {
            var Data = await Redis.StringGetAsync(Key);

            if(!string.IsNullOrEmpty(Data))
            {
                return Data.ToString();
            }

            return null;
        }
    }
}
