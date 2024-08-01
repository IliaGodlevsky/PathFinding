using AutoMapper;
using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Requests.Create;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Mappings
{
    public sealed class HistoryMappingProfile<T> : Profile
        where T : IVertex
    {
        public HistoryMappingProfile()
        {
            CreateMap<PathfindingHistoryModel<T>, PathfindingHistorySerializationModel>();
            CreateMap<CreatePathfindingHistoriesFromSerializationRequest, CreatePathfindingHistoriesRequest<T>>();
            CreateMap<PathfindingHistorySerializationModel, CreatePathfindingHistoryRequest<T>>();
            CreateMap<CreatePathfindingHistoryRequest<T>, PathfindingHistoryModel<T>>();
            CreateMap<PathfindingHistoryModel<T>, CreatePathfindingHistoryRequest<T>>();
            CreateMap<IEnumerable<PathfindingHistoryModel<T>>, PathfindingHistoriesModel<T>>()
                .ConstructUsing(x => new PathfindingHistoriesModel<T>() { Models = x.ToList() });
        }
    }
}
