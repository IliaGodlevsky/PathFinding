using AutoMapper;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IUnitOfWorkExtensions
    {
        public static IReadOnlyCollection<AlgorithmReadDto> GetAlgorithms(this IUnitOfWork unitofWork,
            int graphId, IMapper mapper)
        {
            var algorithms = unitofWork.AlgorithmsRepository.GetByGraphId(graphId);
            return mapper.Map<AlgorithmReadDto[]>(algorithms).ToReadOnly();
        }

        public static IGraph<Vertex> CreateGraph(this IUnitOfWork unitOfWork,
            int graphId, IMapper mapper)
        {
            var graphEntity = unitOfWork.GraphRepository.GetGraph(graphId);
            var vertexEntities = unitOfWork.VerticesRepository
                .GetVerticesByGraphId(graphId)
                .ToDictionary(x => x.Id);
            var ids = vertexEntities.Select(x => x.Key).ToArray();
            var neighbors = unitOfWork.NeighborsRepository
                .GetNeighboursForVertices(ids)
                .ToDictionary(x => x.Key, x => x.Value.Select(i => vertexEntities[i.NeighborId]));
            var readDto = new GraphReadDto()
            {
                Id = graphEntity.Id,
                Width = graphEntity.Width,
                Length = graphEntity.Length,
                Vertices = mapper.Map<VertexReadDto[]>(vertexEntities.Values).ToReadOnly(),
                Neighborhood = neighbors.ToDictionary(x => x.Key, x => mapper.Map<VertexReadDto[]>(x.Value).ToReadOnly())
            };
            return mapper.Map<IGraph<Vertex>>(readDto);
        }

        public static IReadOnlyCollection<ICoordinate> GetRange(this IUnitOfWork unitOfWork, int graphId)
        {
            return unitOfWork.RangeRepository
                .GetByGraphId(graphId)
                .OrderBy(x => x.Position)
                .Select(x => unitOfWork.VerticesRepository.GetVertexById(x.VertexId))
                .Select(x => new Coordinate(x.X, x.Y))
                .ToReadOnly();
        }

        public static int AddGraph(this IUnitOfWork unitOfWork,
            IMapper mapper, IGraph<Vertex> graph)
        {
            var graphEnitity = mapper.Map<GraphEntity>(graph);
            unitOfWork.GraphRepository.AddGraph(graphEnitity);
            var vertices = mapper.Map<IEnumerable<VertexEntity>>(graph)
                .ForEach(x => x.GraphId = graphEnitity.Id)
                .ToReadOnly();
            unitOfWork.VerticesRepository.AddVertices(vertices);
            vertices.Zip(graph, (x, y) => (Entity: x, Vertex: y))
                .ForEach(x => x.Vertex.Id = x.Entity.Id);
            var neighbours = graph
                .SelectMany(x => x.Neighbours.OfType<Vertex>().Select(n => new NeighborEntity()
                {
                    VertexId = x.Id,
                    NeighborId = n.Id
                })).ToArray();
            unitOfWork.NeighborsRepository.AddNeighbours(neighbours);
            return graphEnitity.Id;
        }
    }
}
