using AutoMapper;
using SkyGateDomainLayer.DTOs.AirplaneModule;
using SkyGateDomainLayer.Entities.AirplaneModule;
using SkyGateDomainLayer.Interfaces.AirplaneModule;
using SkyGateDomainLayer.Interfaces.Specifications;
using SkyGateDomainLayer.Interfaces.UnitOfWork;
using SkyGateRepositoryLayer.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateServiceLayer.Services.AirplaneModule
{
    public class AirplaneService : IAirplaneService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;

        public AirplaneService(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            this.UnitOfWork = UnitOfWork;
            this.Mapper = Mapper;
        }
        public int AddAirplane(AirplaneDTO Airplane)
        {
            var AirplaneData = Mapper.Map<Airplane>(Airplane);

            UnitOfWork.AirplaneRepository().Add(AirplaneData);

            return UnitOfWork.SaveChanges();
        }

        public int DeleteAirplane(int AirplaneId)
        {
            UnitOfWork.AirplaneRepository().Delete(AirplaneId);

            return UnitOfWork.SaveChanges();
        }

        public AirplaneDTO GetAirplaneById(int AirplaneId)
        {
            var Spec = new Specification<Airplane, int>();

            Spec.Criteria = x => x.Id == AirplaneId;

            var AiplaneFromDB = UnitOfWork.AirplaneRepository().GetById(Spec);

            if(AiplaneFromDB is not null)
            {
                var Airplane = Mapper.Map<AirplaneDTO>(AiplaneFromDB);

                return Airplane;
            }
            else
            {
                return null;
            }
        }

        public List<AirplaneDTO> GetAllAirplanes()
        {
            var Spec = new Specification<Airplane, int>();

            var AirplanesFromDb = UnitOfWork.AirplaneRepository().GetAll(Spec);

            if(AirplanesFromDb is not null)
            {
                var Airplanes = Mapper.Map<List<AirplaneDTO>>(AirplanesFromDb);

                return Airplanes;
            }

            return null;
        }

        public List<AirplaneDTO> GetAllFreeAirplanes()
        {
            var Spec = new Specification<Airplane, int>();

            Spec.Includes.Add(P => P.Flights);

            var AirplanesFromDb = UnitOfWork.AirplaneRepository().GetAll(Spec);

            var Airplanes = Mapper.Map<List<AirplaneDTO>>(AirplanesFromDb);

            return Airplanes;
        }

        public int UpdateAirplane(AirplaneDTO Airplane)
        {
            var AirplaneToDB = Mapper.Map<Airplane>(Airplane);

            UnitOfWork.AirplaneRepository().Update(AirplaneToDB);

            return UnitOfWork.SaveChanges();
        }
    }
}
