using AutoMapper;
using Pathfinding.Domain.Core;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using System;

namespace Pathfinding.Infrastructure.Business.Mappings
{
    public sealed class StatisticsMappingProfile : Profile
    {
        public StatisticsMappingProfile()
        {
            CreateMap<CreateStatisticsRequest, Statistics>()
                .ForMember(x => x.Elapsed, opt => opt.MapFrom(x => x.Elapsed.TotalMilliseconds));
            CreateMap<RunStatisticsModel, Statistics>()
                .ForMember(x => x.Elapsed, opt => opt.MapFrom(x => x.Elapsed.TotalMilliseconds));
            CreateMap<Statistics, RunStatisticsModel>()
                .ForMember(x => x.Elapsed, opt => opt.MapFrom(x => TimeSpan.FromMilliseconds(x.Elapsed)));
            CreateMap<RunStatisticsModel, RunStatisticsSerializationModel>();
            CreateMap<RunStatisticsSerializationModel, RunStatisticsModel>();
            CreateMap<RunStatisticsSerializationModel, CreateStatisticsRequest>();
        }
    }
}
