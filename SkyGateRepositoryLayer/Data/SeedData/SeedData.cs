using SkyGateDomainLayer.Entities.AirplaneModule;
using SkyGateDomainLayer.Entities.FlightModule;
using SkyGateRepositoryLayer.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkyGateRepositoryLayer.Data.SeedData
{
    public static class SeedData
    {
        public static void Seed(AppDbContext Context)
        {
            if(Context.Airplanes.Count() == 0)
            {
                var DataFromFile = File.ReadAllText("../SkyGateRepositoryLayer/Data/SeedData/AirplaneModule/Airplanes.json");

                if(DataFromFile is not null)
                {
                    var DataToObjects = JsonSerializer.Deserialize<List<Airplane>>(DataFromFile);

                    if(DataToObjects is not null)
                    {
                        Context.Airplanes.AddRange(DataToObjects);
                        Context.SaveChanges();
                    }
                }
            }

            if(Context.Flights.Count() == 0)
            {
                var DataFromFile = File.ReadAllText("../SkyGateRepositoryLayer/Data/SeedData/FlightModule/Flights.json");

                if(DataFromFile is not null)
                {
                    var DataToObjects = JsonSerializer.Deserialize<List<Flight>>(DataFromFile);

                    if(DataToObjects is not null)
                    {
                        Context.Flights.AddRange(DataToObjects);
                        Context.SaveChanges();
                    }
                }
            }
        }
    }
}
