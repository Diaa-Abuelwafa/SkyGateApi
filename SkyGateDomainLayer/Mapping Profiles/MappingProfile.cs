using AutoMapper;
using SkyGateDomainLayer.DTOs.AirplaneModule;
using SkyGateDomainLayer.DTOs.FlightModule;
using SkyGateDomainLayer.DTOs.Identity;
using SkyGateDomainLayer.Entities.AirplaneModule;
using SkyGateDomainLayer.Entities.FlightModule;
using SkyGateDomainLayer.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGateDomainLayer.Mapping_Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDTO, ApplicationUser>();
            CreateMap<ApplicationUser, RegisterDTO>();

            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<UserDTO, ApplicationUser>();

            CreateMap<Airplane, AirplaneDTO>();
            CreateMap<AirplaneDTO, Airplane>();

            CreateMap<Flight, FlightDTO>();
            CreateMap<FlightDTO, Flight>();

            CreateMap<FlightCreateDTO, Flight>();
            CreateMap<Flight, FlightCreateDTO>();
        }
    }
}
