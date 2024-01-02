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

namespace Pathfinding.App.Console.DAL.Services
{
    internal sealed class CacheService : IService
    {
        private readonly IMapper mapper;
        private readonly IService service;

        private bool areAllGraphsFetched = false;
        private readonly HashSet<int> areAllAlgorithmsFetched = new();

        private readonly Dictionary<int, IGraph<Vertex>> graphs = new();
        private readonly Dictionary<int, List<AlgorithmReadDto>> algorithms = new();
        private readonly Dictionary<int, List<ICoordinate>> range = new();
        private readonly Dictionary<int, GraphEntity> graphEntities = new();

        public CacheService(IService service, IMapper mapper)
        {
            this.mapper = mapper;
            this.service = service;
        }

        public int AddAlgorithm(AlgorithmCreateDto algorithm)
        {
            int graphId = algorithm.GraphId;
            int id = service.AddAlgorithm(algorithm);
            var readDto = mapper.Map<AlgorithmReadDto>(algorithm);
            readDto.Id = id;
            algorithms.TryGetOrAddNew(graphId).Add(readDto);
            return id;
        }

        public int AddGraph(IGraph<Vertex> graph)
        {
            int id = service.AddGraph(graph);
            var entity = mapper.Map<GraphEntity>(graph);
            entity.Id = id;
            graphEntities.Add(id, entity);
            graphs[id] = graph;
            return id;
        }

        public PathfindingHistoryReadDto AddPathfindingHistory(PathfindingHistoryCreateDto history)
        {
            var dto = service.AddPathfindingHistory(history);
            graphs[dto.Id] = dto.Graph;
            var entity = mapper.Map<GraphEntity>(dto.Graph);
            entity.Id = dto.Id;
            graphEntities.Add(dto.Id, entity);
            algorithms.TryGetOrAddNew(dto.Id).AddRange(dto.Algorithms);
            range.TryGetOrAddNew(dto.Id).AddRange(history.Range);
            return dto;
        }

        public bool DeleteGraph(int graphId)
        {
            bool deleted = service.DeleteGraph(graphId);
            if (deleted)
            {
                graphs.Remove(graphId);
                algorithms.Remove(graphId);
                range.Remove(graphId);
                graphEntities.Remove(graphId);
                areAllAlgorithmsFetched.Remove(graphId);
            }
            return deleted;
        }

        public IReadOnlyList<GraphEntity> GetAllGraphInfo()
        {
            if (!areAllGraphsFetched)
            {
                var entities = service.GetAllGraphInfo();
                foreach (var value in entities)
                {
                    graphEntities[value.Id] = value;
                }
                areAllGraphsFetched = true;
                return entities;
            }
            return graphEntities.Values.ToList().AsReadOnly();
        }

        public IGraph<Vertex> GetGraph(int id)
        {
            var graph = graphs.GetOrDefault(id, Graph<Vertex>.Empty);
            if (graph == Graph<Vertex>.Empty)
            {
                graph = service.GetGraph(id);
                graphs[id] = graph;
            }
            return graph;
        }

        public IReadOnlyCollection<int> GetGraphIds()
        {
            return GetAllGraphInfo().Select(x => x.Id).ToReadOnly();
        }

        public IReadOnlyCollection<AlgorithmReadDto> GetGraphPathfindingHistory(int graphId)
        {
            if (!areAllAlgorithmsFetched.Contains(graphId))
            {
                var history = service.GetGraphPathfindingHistory(graphId);
                areAllAlgorithmsFetched.Add(graphId);
                algorithms[graphId] = history.ToList();
                return history;
            }
            return algorithms.GetOrEmpty(graphId);
        }

        public PathfindingHistoryReadDto GetPathfindingHistory(int graphId)
        {
            var graph = GetGraph(graphId);
            var algorithms = GetGraphPathfindingHistory(graphId);
            var range = GetRange(graphId);
            return new PathfindingHistoryReadDto()
            {
                Id = graphId,
                Graph = graph,
                Algorithms = algorithms,
                Range = range
            };
        }

        public IReadOnlyCollection<ICoordinate> GetRange(int graphId)
        {
            var pathfindingRange = range.TryGetOrAddNew(graphId);
            if (pathfindingRange.Count == 0)
            {
                pathfindingRange = service.GetRange(graphId).ToList();
                range[graphId] = pathfindingRange;
            }
            return pathfindingRange.AsReadOnly();
        }

        public bool RemoveRange(int graphId)
        {
            bool isDeleted = service.RemoveRange(graphId);
            if (isDeleted)
            {
                range.Remove(graphId);
            }
            return isDeleted;
        }

        public bool AddRange((int Order, Vertex Vertex)[] vertices, int graphId)
        {
            bool added = service.AddRange(vertices, graphId);
            if (added)
            {
                foreach (var vertex in vertices)
                {
                    range.TryGetOrAddNew(graphId)
                        .Insert(vertex.Order, vertex.Vertex.Position);
                }
            }
            return added;
        }

        public bool RemoveRange(IEnumerable<Vertex> vertices, int graphId)
        {
            bool removed = service.RemoveRange(vertices, graphId);
            if (removed)
            {
                foreach (var vertex in vertices)
                {
                    range.TryGetOrAddNew(graphId).Remove(vertex.Position);
                }
            }
            return removed;
        }

        public bool UpdateRange((int Order, Vertex Vertex)[] vertices, int graphId)
        {
            bool updated = service.UpdateRange(vertices, graphId);
            if (updated)
            {
                var currentRange = range.TryGetOrAddNew(graphId);
                foreach (var vertex in vertices)
                {
                    currentRange.Remove(vertex.Vertex.Position);
                    currentRange.Insert(vertex.Order, vertex.Vertex.Position);
                }
            }
            return updated;
        }

        public bool UpdateObstaclesCount(int newCount, int graphId)
        {
            graphEntities[graphId].ObstaclesCount = newCount;
            return service.UpdateObstaclesCount(newCount, graphId);
        }

        public PathfindingHistoryReadDto AddPathfindingHistory(PathfindingHistorySerializationDto history)
        {
            var dto = mapper.Map<PathfindingHistoryCreateDto>(history);
            return AddPathfindingHistory(dto);
        }

        public PathfindingHistorySerializationDto GetSerializationHistory(int graphId)
        {
            var history = GetPathfindingHistory(graphId);
            return mapper.Map<PathfindingHistorySerializationDto>(history);
        }

        public GraphSerializationDto GetSerializationGraph(int graphId)
        {
            var graph = GetGraph(graphId);
            return mapper.Map<GraphSerializationDto>(graph);
        }

        public int AddGraph(GraphSerializationDto graph)
        {
            var add = mapper.Map<IGraph<Vertex>>(graph);
            return AddGraph(add);
        }

        public bool AddNeighbors(IReadOnlyDictionary<int, int[]> neighborhoods)
        {
            return service.AddNeighbors(neighborhoods);
        }

        public bool UpdateVertices(IEnumerable<Vertex> vertices, int graphId)
        {
            return service.UpdateVertices(vertices, graphId);
        }

        public bool RemoveNeighbors(IReadOnlyDictionary<int, int[]> neighborhoods)
        {
            return service.RemoveNeighbors(neighborhoods);
        }
    }
}
