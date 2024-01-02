using AutoMapper;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Models.Mappers
{
    internal sealed class GraphMappingProfile : Profile
    {
        private readonly IGraphFactory<Vertex> graphFactory;

        public GraphMappingProfile(IGraphFactory<Vertex> graphFactory)
        {
            this.graphFactory = graphFactory;

            CreateMap<IGraph<Vertex>, IGraph<Vertex>>().ConvertUsing(x => x);
            CreateMap<IGraph<Vertex>, GraphEntity>()
                .ForMember(x => x.Width, opt => opt.MapFrom(x => x.GetWidth()))
                .ForMember(x => x.Length, opt => opt.MapFrom(x => x.GetLength()))
                .ForMember(x => x.ObstaclesCount, opt => opt.MapFrom(x => x.GetObstaclesCount()));
            CreateMap<GraphReadDto, IGraph<Vertex>>().ConstructUsing((x, context) =>
            {
                var vertices = context.Mapper.Map<Vertex[]>(x.Vertices);
                var paramemters = new[] { x.Width, x.Length };
                var graph = graphFactory.CreateGraph(vertices, paramemters);
                graph.ForEach(vertex =>
                {
                    var coordinates = x.Neighborhood[vertex.Id]
                        .Select(i => graph.Get(i.Coordinate));
                    vertex.Neighbours.AddRange(coordinates);
                });
                return graph;
            });
            CreateMap<IGraph<Vertex>, IReadOnlyCollection<VertexSerializationDto>>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<VertexSerializationDto[]>(x.AsEnumerable()));
            CreateMap<IGraph<Vertex>, GraphSerializationDto>()
                .ForMember(x => x.DimensionSizes, opt => opt.MapFrom(x => x.DimensionsSizes))
                .ForMember(x => x.Vertices, opt => opt.MapFrom(x => x.ToArray()));
            CreateMap<GraphSerializationDto, IGraph<Vertex>>()
                .ConstructUsing((x, context) =>
                {
                    var vertices = context.Mapper
                        .Map<IEnumerable<Vertex>>(x.Vertices)
                        .ToReadOnly();
                    return graphFactory.CreateGraph(vertices, x.DimensionSizes);
                });
        }
    }
}
