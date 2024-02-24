using AutoMapper;
using Pathfinding.App.Console.DAL.Models.Entities;
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
            CreateMap<IGraph<T>, GraphEntity>()
                .ForMember(x => x.Width, opt => opt.MapFrom(x => x.GetWidth()))
                .ForMember(x => x.Length, opt => opt.MapFrom(x => x.GetLength()))
                .ForMember(x => x.ObstaclesCount, opt => opt.MapFrom(x => x.GetObstaclesCount()));
            CreateMap<GraphAssembleDto, IGraph<T>>().ConstructUsing(Construct);
            CreateMap<IGraph<T>, IReadOnlyCollection<VertexSerializationDto>>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<VertexSerializationDto[]>(x.AsEnumerable()));
            CreateMap<IGraph<T>, GraphSerializationDto>()
                .ForMember(x => x.DimensionSizes, opt => opt.MapFrom(x => x.DimensionsSizes))
                .ForMember(x => x.Vertices, opt => opt.MapFrom(x => x.ToArray()));
            CreateMap<GraphSerializationDto, IGraph<T>>().ConstructUsing(Construct);
        }

        private IGraph<T> Construct(GraphSerializationDto serializationDto, ResolutionContext context)
        {
            var vertices = context.Mapper
                        .Map<IEnumerable<T>>(serializationDto.Vertices)
                        .ToReadOnly();
            return graphFactory.CreateGraph(vertices, serializationDto.DimensionSizes);
        }

        private IGraph<T> Construct(GraphAssembleDto assembleDto, ResolutionContext context)
        {
            var vertices = context.Mapper.Map<T[]>(assembleDto.Vertices);
            var paramemters = new[] { assembleDto.Width, assembleDto.Length };
            var graph = graphFactory.CreateGraph(vertices, paramemters);
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
