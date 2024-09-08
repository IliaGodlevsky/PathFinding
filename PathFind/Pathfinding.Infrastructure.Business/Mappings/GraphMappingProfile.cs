using AutoMapper;
using Newtonsoft.Json;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Mappings
{
    public sealed class GraphMappingProfile<T> : Profile
        where T : IVertex
    {
        private readonly IGraphFactory<T> graphFactory;

        public GraphMappingProfile(IGraphFactory<T> graphFactory)
        {
            this.graphFactory = graphFactory;
            CreateMap<IGraph<T>, IGraph<T>>().ConvertUsing(x => x);
            CreateMap<GraphAssembleModel, IGraph<T>>().ConstructUsing(Construct);
            CreateMap<IGraph<T>, IReadOnlyCollection<VertexSerializationModel>>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<VertexSerializationModel[]>(x.AsEnumerable()));
            CreateMap<IGraph<T>, GraphSerializationModel>()
                .ForMember(x => x.DimensionSizes, opt => opt.MapFrom(x => x.DimensionsSizes))
                .ForMember(x => x.Vertices, opt => opt.MapFrom(x => x.ToArray()));
            CreateMap<GraphSerializationModel, CreateGraphRequest<T>>().ConstructUsing(Construct);
            CreateMap<CreateGraphRequest<T>, Graph>()
                .ForMember(x => x.Dimensions, opt => opt.MapFrom(x => JsonConvert.SerializeObject(x.Graph.DimensionsSizes)))
                .ForMember(x => x.ObstaclesCount, opt => opt.MapFrom(x => x.Graph.GetObstaclesCount()));
            CreateMap<CreateGraphRequest<T>, GraphModel<T>>();
            CreateMap<GraphModel<T>, CreateGraphRequest<T>>();
            CreateMap<GraphModel<T>, GraphSerializationModel>()
                .ForMember(x => x.DimensionSizes, opt => opt.MapFrom(x => x.Graph.DimensionsSizes))
                .ForMember(x => x.Vertices, opt => opt.MapFrom(x => x.Graph.ToArray()))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name));
            CreateMap<CreateGraphFromSerializationRequest, CreateGraphRequest<T>>();
            CreateMap<CreateGraphRequest<T>, GraphSerializationModel>()
                .ConvertUsing((x, y, context) => context.Mapper.Map<GraphSerializationModel>(x.Graph) with { Name = x.Name });
            CreateMap<Graph, GraphInformationModel>()
                .ForMember(x => x.Dimensions, opt => opt.MapFrom(x => JsonConvert.DeserializeObject<int[]>(x.Dimensions)));
        }

        private CreateGraphRequest<T> Construct(GraphSerializationModel serializationDto, ResolutionContext context)
        {
            var vertices = context.Mapper
                        .Map<IEnumerable<T>>(serializationDto.Vertices)
                        .ToReadOnly();
            var graph = graphFactory.CreateGraph(vertices, serializationDto.DimensionSizes);
            return new() { Name = serializationDto.Name, Graph = graph };
        }

        private IGraph<T> Construct(GraphAssembleModel assembleDto, ResolutionContext context)
        {
            var vertices = context.Mapper.Map<T[]>(assembleDto.Vertices).ToArray();
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
