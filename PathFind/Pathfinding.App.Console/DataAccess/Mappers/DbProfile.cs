using AutoMapper;
using Pathfinding.App.Console.DataAccess.Dto;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.Decorators;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using System.Collections.Generic;
using Newtonsoft.Json;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Extensions;
using Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories;
using System;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Mappers
{
    internal sealed class DbProfile : Profile
    {
        private readonly CompressSerializer<IEnumerable<ICoordinate>> coordinateSerializer;
        private readonly CompressSerializer<IEnumerable<int>> arraySerializer;
        private readonly IVertexFactory<Vertex> vertexFactory;
        private readonly IGraphFactory<Vertex> graphFactory;

        public DbProfile(ISerializer<IEnumerable<ICoordinate>> coordinateSerializer,
            ISerializer<IEnumerable<int>> arraySerializer,
            IVertexFactory<Vertex> vertexFactory, 
            IGraphFactory<Vertex> graphFactory)
        {
            this.coordinateSerializer = new(coordinateSerializer);
            this.arraySerializer = new(arraySerializer);
            this.vertexFactory = vertexFactory;
            this.graphFactory = graphFactory;

            CreateMap<byte[], IReadOnlyCollection<ICoordinate>>()
                .ConvertUsing(x => this.coordinateSerializer.DeserializeFromBytes(x).ToReadOnly());
            CreateMap<IReadOnlyCollection<ICoordinate>, byte[]>()
                .ConvertUsing(x => this.coordinateSerializer.SerializeToBytes(x));
            CreateMap<AlgorithmEntity, AlgorithmReadDto>()
                .ForMember(x => x.Costs, opt => opt.MapFrom(x => FromBytesToCosts(x.Costs)))
                .ForMember(x => x.Statistics, opt => opt.MapFrom(x => FromStringToStatistics(x.Statistics)));
            CreateMap<AlgorithmCreateDto, AlgorithmEntity>()
                .ForMember(x => x.Costs, opt => opt.MapFrom(x => FromCostsToBytes(x.Costs)))
                .ForMember(x => x.Statistics, opt => opt.MapFrom(x => FromStatisticsToString(x.Statistics)));
            CreateMap<ICoordinate, ICoordinate>().ConvertUsing(x => x);
            CreateMap<IGraph<Vertex>, IGraph<Vertex>>().ConvertUsing(x => x);
            CreateMap<VertexReadDto, Vertex>().ConstructUsing(x => vertexFactory.CreateVertex(x.Coordinate));
            CreateMap<Vertex, VertexEntity>()
                .ForMember(x => x.X, opt => opt.MapFrom(x => x.Position.GetX()))
                .ForMember(x => x.Y, opt => opt.MapFrom(x => x.Position.GetY()))
                .ForMember(x => x.UpperValueRange, opt => opt.MapFrom(x => x.Cost.CostRange.UpperValueOfRange))
                .ForMember(x => x.LowerValueRange, opt => opt.MapFrom(x => x.Cost.CostRange.LowerValueOfRange))
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => x.Cost.CurrentCost));
            CreateMap<VertexEntity, Vertex>().ConstructUsing(x => vertexFactory.CreateVertex(new Coordinate(x.X, x.Y)))
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => new VertexCost(x.Cost, new(x.UpperValueRange, x.LowerValueRange))));
            CreateMap<VertexEntity, VertexReadDto>()
                .ForMember(x => x.Coordinate, opt => opt.MapFrom(x => new Coordinate(x.X, x.Y)))
                .ForMember(x => x.Cost, opt => opt.MapFrom(x => new VertexCost(x.Cost, new(x.UpperValueRange, x.LowerValueRange))));
            CreateMap<AlgorithmReadDto, AlgorithmSerializationDto>();
            CreateMap<AlgorithmSerializationDto, AlgorithmCreateDto>();
            CreateMap<AlgorithmCreateDto, AlgorithmReadDto>();
            CreateMap<PathfindingHistoryReadDto, PathfindingHistorySerializationDto>();
            CreateMap<PathfindingHistorySerializationDto, PathfindingHistoryCreateDto>();
            CreateMap<PathfindingHistoryCreateDto, PathfindingHistoryReadDto>();
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
                    var coordinates = x.Neighborhood[vertex.Id].Select(i => i.Coordinate);
                    vertex.Neighbours.AddRange(coordinates.Select(graph.Get));
                });
                return graph;
            });
        }

        private IReadOnlyCollection<int> FromBytesToCosts(byte[] str)
        {
            return arraySerializer.DeserializeFromBytes(str).ToReadOnly();
        }

        private Statistics FromStringToStatistics(string str)
        {
            return JsonConvert.DeserializeObject<Statistics>(str);
        }

        private byte[] FromCostsToBytes(IEnumerable<int> costs)
        {
            return arraySerializer.SerializeToBytes(costs);
        }

        private string FromStatisticsToString(Statistics statistics)
        {
            return JsonConvert.SerializeObject(statistics);
        }
    }
}
