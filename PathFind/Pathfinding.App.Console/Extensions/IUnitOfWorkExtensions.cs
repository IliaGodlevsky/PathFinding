using AutoMapper;
using Pathfinding.App.Console.DataAccess.Dto;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.DataAccess.UnitOfWorks;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IUnitOfWorkExtensions
    {
        public static GraphReadDto GetGraph(this IUnitOfWork unitofWork, 
            int graphId, IMapper mapper)
        {
            var graphEntity = unitofWork.GraphRepository.GetGraph(graphId);
            var vertexEntities = unitofWork.VerticesRepository
                .GetVerticesByGraphId(graphId)
                .ToDictionary(x => x.Id);
            var ids = vertexEntities.Select(x => x.Key).ToArray();
            var neighbors = unitofWork.NeighborsRepository
                .GetNeighboursForVertices(ids)
                .ToDictionary(x => x.Key, x => x.Value.Select(i => vertexEntities[i.NeighbourId]));
            return new GraphReadDto()
            {
                Id = graphId,
                Width = graphEntity.Width,
                Length = graphEntity.Length,
                Vertices = mapper.Map<VertexReadDto[]>(vertexEntities.Values).ToArray(),
                Neighborhood = neighbors.ToDictionary(x => x.Key, x => (IReadOnlyCollection<VertexReadDto>)mapper.Map<VertexReadDto[]>(x.Value))
            };
        }

        public static IReadOnlyCollection<AlgorithmReadDto> GetAlgorithms(this IUnitOfWork unitofWork,
            int graphId, IMapper mapper)
        {
            var algorithms = unitofWork.AlgorithmsRepository.GetByGraphId(graphId);
            return Array.AsReadOnly(mapper.Map<AlgorithmReadDto[]>(algorithms));
        }

        public static IGraph<Vertex> CreateGraph(this IUnitOfWork unitOfWork,
            int graphId, IMapper mapper,
            IGraphFactory<Vertex> graphFactory)
        {
            var graphInfo = unitOfWork.GetGraph(graphId, mapper);
            var vertices = mapper.Map<Vertex[]>(graphInfo.Vertices);
            var parametres = new[] { graphInfo.Width, graphInfo.Length };
            var graph = graphFactory.CreateGraph(vertices, parametres);
            graph.ForEach(vertex =>
            {
                var coordinates = graphInfo.Neighborhood[vertex.Id].Select(i => i.Coordinate);
                vertex.Neighbours.AddRange(coordinates.Select(graph.Get));
            });
            return graph;
        }

        public static IReadOnlyCollection<ICoordinate> GetRange(this IUnitOfWork unitOfWork, int graphId)
        {
            var range = unitOfWork.RangeRepository
                .GetByGraphId(graphId)
                .OrderBy(x => x.Order)
                .Select(x => unitOfWork.VerticesRepository.GetVertexById(x.VertexId))
                .Select(x => new Coordinate(x.X, x.Y))
                .OfType<ICoordinate>()
                .ToArray();
            return Array.AsReadOnly(range);
        }

        public static int AddGraph(this IUnitOfWork unitOfWork,
            IMapper mapper, IGraph<Vertex> graph)
        {
            var graphEnitity = new GraphEntity()
            {
                ObstaclesCount = graph.GetObstaclesCount(),
                Width = graph.GetWidth(),
                Length = graph.GetLength()
            };
            unitOfWork.GraphRepository.AddGraph(graphEnitity);
            var vertices = mapper.Map<IEnumerable<Vertex>, IEnumerable<VertexEntity>>(graph)
                .ForEach(x => x.GraphId = graphEnitity.Id)
                .ToArray();
            unitOfWork.VerticesRepository.AddVertices(vertices);
            vertices.Zip(graph, (x, y) => (Entity: x, Vertex: y))
                .ForEach(x => x.Vertex.Id = x.Entity.Id);
            var neighbours = graph
                .SelectMany(x => x.Neighbours.OfType<Vertex>().Select(n => new NeighbourEntity()
                {
                    VertexId = x.Id,
                    NeighbourId = n.Id
                })).ToArray();
            unitOfWork.NeighborsRepository.AddNeighbours(neighbours);
            return graphEnitity.Id;
        }

        public static void AddRange(this IUnitOfWork unitOfWork, 
            IGraph<Vertex> graph, IEnumerable<ICoordinate> range, int graphId)
        {
            var entities = range.Select((x,i) =>
            {
                var vertex = graph.Get(x);
                return new RangeEntity
                {
                    VertexId = vertex.Id,
                    GraphId = graphId,
                    Order = i
                };
            });
            unitOfWork.RangeRepository.AddRange(entities);
        }

        public static bool DeleteGraph(this IUnitOfWork unitOfWork, int graphId)
        {
            unitOfWork.NeighborsRepository.DeleteByGraphId(graphId);
            unitOfWork.GraphRepository.DeleteGraph(graphId);
            unitOfWork.AlgorithmsRepository.DeleteByGraphId(graphId);
            unitOfWork.VerticesRepository.DeleteVerticesByGraphId(graphId);
            unitOfWork.RangeRepository.DeleteByGraphId(graphId);
            return true;
        }
    }
}
