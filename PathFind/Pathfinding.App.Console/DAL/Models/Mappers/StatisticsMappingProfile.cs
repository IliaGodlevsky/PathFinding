using AutoMapper;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;

namespace Pathfinding.App.Console.DAL.Models.Mappers
{
    internal sealed class StatisticsMappingProfile : Profile
    {
        public StatisticsMappingProfile() 
        { 
            CreateMap<RunStatisticsDto, StatisticsEntity>();
            CreateMap<StatisticsEntity, RunStatisticsDto>();
            CreateMap<RunStatisticsDto, RunStatisticsSerializationDto>();
            CreateMap<RunStatisticsSerializationDto, RunStatisticsDto>();
        }
    }
}
