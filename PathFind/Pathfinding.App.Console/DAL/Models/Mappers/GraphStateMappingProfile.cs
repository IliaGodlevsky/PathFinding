using AutoMapper;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Create;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;

namespace Pathfinding.App.Console.DAL.Models.Mappers
{
    internal sealed class GraphStateMappingProfile : Profile
    {
        public GraphStateMappingProfile()
        {
            CreateMap<GraphStateCreateDto, GraphStateReadDto>();
            CreateMap<GraphStateReadDto, GraphStateCreateDto>();

            CreateMap<GraphStateCreateDto, GraphStateEntity>();
            CreateMap<GraphStateEntity, GraphStateReadDto>();

            CreateMap<GraphStateReadDto, GraphStateSerializationDto>();
            CreateMap<GraphStateSerializationDto, GraphStateReadDto>();
            CreateMap<GraphStateSerializationDto, GraphStateCreateDto>();
        }
    }
}
