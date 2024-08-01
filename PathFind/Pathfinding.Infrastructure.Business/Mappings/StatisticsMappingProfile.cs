using AutoMapper;
using Pathfinding.Domain.Core;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;

namespace Pathfinding.Infrastructure.Business.Mappings
{
    public sealed class StatisticsMappingProfile : Profile
    {
        public StatisticsMappingProfile()
        {
            CreateMap<RunStatisticsModel, Statistics>();
            CreateMap<Statistics, RunStatisticsModel>();
            CreateMap<RunStatisticsModel, RunStatisticsSerializationModel>();
            CreateMap<RunStatisticsSerializationModel, RunStatisticsModel>();
        }
    }
}
