using SkyGateDomainLayer.DTOs.AirplaneModule;
using SkyGateDomainLayer.Entities.AirplaneModule;
using SkyGateDomainLayer.Interfaces.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Interfaces.AirplaneModule
{
    public interface IAirplaneService
    {
        public List<AirplaneDTO> GetAllAirplanes();
        public List<AirplaneDTO> GetAllFreeAirplanes();
        public AirplaneDTO GetAirplaneById(int AirplaneId);
        public int AddAirplane(AirplaneDTO Airplane);
        public int UpdateAirplane(AirplaneDTO Airplane);
        public int DeleteAirplane(int AirplaneId);
    }
}
