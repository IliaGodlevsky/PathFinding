using AutoMapper;
using Newtonsoft.Json;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Create;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Models.Mappers
{
    internal sealed class GraphMappingProfile<T> : Profile
        where T : IVertex
    {
        private readonly IGraphFactory<T> graphFactory;

        public GraphMappingProfile(IGraphFactory<T> graphFactory)
        {
            this.graphFactory = graphFactory;
            CreateMap<IGraph<T>, IGraph<T>>().ConvertUsing(x => x);
            CreateMap<GraphAssembleDto, IGraph<T>>().ConstructUsing(Construct);
            CreateMap<IGraph<T>, IReadOnlyCollection<VertexSerializationDto>>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<VertexSerializationDto[]>(x.AsEnumerable()));
            CreateMap<IGraph<T>, GraphSerializationDto>()
                .ForMember(x => x.DimensionSizes, opt => opt.MapFrom(x => x.DimensionsSizes))
                .ForMember(x => x.Vertices, opt => opt.MapFrom(x => x.ToArray()));
            CreateMap<GraphSerializationDto, GraphCreateDto<T>>().ConstructUsing(Construct);
            CreateMap<GraphCreateDto<T>, GraphEntity>()
                .ForMember(x => x.Dimensions, opt => opt.MapFrom(x => JsonConvert.SerializeObject(x.Graph.DimensionsSizes)))
                .ForMember(x => x.ObstaclesCount, opt => opt.MapFrom(x => x.Graph.GetObstaclesCount()));
            CreateMap<GraphCreateDto<T>, GraphReadDto<T>>();
            CreateMap<GraphReadDto<T>, GraphSerializationDto>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<GraphSerializationDto>(x.Graph) with { Name = x.Name });
            CreateMap<GraphEntity, GraphInformationReadDto>()
                .ForMember(x => x.Dimensions, opt => opt.MapFrom(x => JsonConvert.DeserializeObject<int[]>(x.Dimensions)));
        }

        private GraphCreateDto<T> Construct(GraphSerializationDto serializationDto, ResolutionContext context)
        {
            var vertices = context.Mapper
                        .Map<IEnumerable<T>>(serializationDto.Vertices)
                        .ToReadOnly();
            var graph = graphFactory.CreateGraph(vertices, serializationDto.DimensionSizes);
            return new() { Name = serializationDto.Name, Graph = graph };
        }

        private IGraph<T> Construct(GraphAssembleDto assembleDto, ResolutionContext context)
        {
            var vertices = context.Mapper.Map<T[]>(assembleDto.Vertices);
            var graph = graphFactory.CreateGraph(vertices, assembleDto.Dimensions);
            vertices.Zip(assembleDto.Vertices, (i, j) => (Vertex: i, Info: j))
                    .ForEach(i =>
                    {
                        var coordinates = assembleDto.Neighborhood[i.Info.Id]
                            .Select(j => graph.Get(j.Coordinate));
                        i.Vertex.Neighbours.AddRange(coordinates.OfType<IVertex>());
                    });
            return graph;
        }
    }
}
