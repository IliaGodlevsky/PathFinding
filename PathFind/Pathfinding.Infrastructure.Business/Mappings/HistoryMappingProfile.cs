using AutoMapper;
using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Requests.Create;

namespace Pathfinding.Infrastructure.Business.Mappings
{
    public sealed class HistoryMappingProfile<T> : Profile
        where T : IVertex
    {
        public HistoryMappingProfile()
        {
            CreateMap<PathfindingHistoryModel<T>, PathfindingHistorySerializationModel>();
            CreateMap<PathfindingHistorySerializationModel, CreatePathfindingHistoryRequest<T>>();
            CreateMap<CreatePathfindingHistoryRequest<T>, PathfindingHistoryModel<T>>();
            CreateMap<PathfindingHistoryModel<T>, CreatePathfindingHistoryRequest<T>>();
        }
    }
}
