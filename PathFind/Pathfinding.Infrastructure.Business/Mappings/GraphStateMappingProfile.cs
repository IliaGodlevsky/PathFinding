using AutoMapper;
using Pathfinding.Domain.Core;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Requests.Create;

namespace Pathfinding.Infrastructure.Business.Mappings
{
    public sealed class GraphStateMappingProfile : Profile
    {
        public GraphStateMappingProfile()
        {
            CreateMap<CreateGraphStateRequest, GraphStateModel>();
            CreateMap<GraphStateModel, CreateGraphStateRequest>();

            CreateMap<CreateGraphStateRequest, GraphState>();
            CreateMap<GraphState, GraphStateModel>();

            CreateMap<GraphStateModel, GraphStateSerializationModel>();
            CreateMap<GraphStateSerializationModel, GraphStateModel>();
            CreateMap<GraphStateSerializationModel, CreateGraphStateRequest>();
        }
    }
}
