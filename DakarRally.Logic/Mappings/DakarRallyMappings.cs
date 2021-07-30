using AutoMapper;
using DakarRally.Data.Models;
using DakarRally.Domain.Dtos;
using DakarRally.Domain.Results;

namespace DakarRally.Logic.Mappings
{
    public class DakarRallyMappings : Profile
    {
        public DakarRallyMappings()
        {
            CreateMap<Vehicle, VehicleDto>()
                .ForMember(x => x.Type, x => x.MapFrom(x => x.Type.Type))
                .ForMember(x => x.SubType, x => x.MapFrom(x => x.Type.SubType));
            CreateMap<HeavyMalfunction, HeavyMalfunctionDto>();
            CreateMap<LightMalfunction, LightMalfunctionDto>();
            CreateMap<VehicleStatistics, VehicleRaceStatus>();
        }
    }
}
