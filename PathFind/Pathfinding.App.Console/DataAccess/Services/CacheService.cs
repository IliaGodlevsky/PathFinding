using Pathfinding.App.Console.DataAccess.Dto;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Services
{
    internal sealed class CacheService : Service
    {
        private bool areAllGraphsFetched = false;
        private readonly HashSet<int> areAllAlgorithmsFetched = new();

        private readonly Dictionary<int, IGraph<Vertex>> graphs = new();
        private readonly Dictionary<int, List<AlgorithmReadDto>> algorithms = new();
        private readonly Dictionary<int, List<ICoordinate>> range = new();
        private readonly Dictionary<int, GraphEntity> graphEntities = new();

        public CacheService(Service service)
            : base(service) 
        {
        }

        public override int AddAlgorithm(AlgorithmCreateDto algorithm)
        {
            int graphId = algorithm.GraphId;
            int id = base.AddAlgorithm(algorithm);
            var readDto = mapper.Map<AlgorithmReadDto>(algorithm);
            readDto.Id = id;
            algorithms.TryGetOrAddNew(graphId).Add(readDto);
            return id;
        }

        public override int AddGraph(IGraph<Vertex> graph)
        {
            int id = base.AddGraph(graph);
            var entity = mapper.Map<GraphEntity>(graph);
            entity.Id = id;
            graphEntities.Add(id, entity);
            graphs[id] = graph;
            return id;
        }

        public override PathfindingHistoryReadDto AddPathfindingHistory(PathfindingHistoryCreateDto history)
        {
            var dto = base.AddPathfindingHistory(history);
            graphs[dto.Id] = dto.Graph;
            var entity = mapper.Map<GraphEntity>(dto.Graph);
            entity.Id = dto.Id;
            graphEntities.Add(dto.Id, entity);
            algorithms.TryGetOrAddNew(dto.Id).AddRange(dto.Algorithms);
            range.TryGetOrAddNew(dto.Id).AddRange(history.Range);
            return dto;
        }

        public override bool DeleteGraph(int graphId)
        {
            bool deleted = base.DeleteGraph(graphId);
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

        public override IReadOnlyList<GraphEntity> GetAllGraphInfo()
        {
            if (!areAllGraphsFetched)
            {
                var entities = base.GetAllGraphInfo();
                foreach (var value in entities)
                {
                    graphEntities[value.Id] = value;
                }
                areAllGraphsFetched = true;
                return entities;
            }
            return graphEntities.Values.ToList().AsReadOnly();
        }

        public override IGraph<Vertex> GetGraph(int id)
        {
            var graph = graphs.GetOrDefault(id, Graph<Vertex>.Empty);
            if (graph == Graph<Vertex>.Empty)
            {
                graph = base.GetGraph(id);
                graphs[id] = graph;
            }
            return graph;
        }

        public override IReadOnlyCollection<int> GetGraphIds()
        {
            return GetAllGraphInfo().Select(x => x.Id).ToReadOnly();
        }

        public override IReadOnlyCollection<AlgorithmReadDto> GetGraphPathfindingHistory(int graphId)
        {
            if (!areAllAlgorithmsFetched.Contains(graphId))
            {
                var history = base.GetGraphPathfindingHistory(graphId);
                areAllAlgorithmsFetched.Add(graphId);
                algorithms[graphId] = history.ToList();
                return history;
            }
            return algorithms.GetOrEmpty(graphId);
        }

        public override PathfindingHistoryReadDto GetPathfindingHistory(int graphId)
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

        public override IReadOnlyCollection<ICoordinate> GetRange(int graphId)
        {
            var pathfindingRange = range.TryGetOrAddNew(graphId);
            if (pathfindingRange.Count == 0)
            {
                pathfindingRange = base.GetRange(graphId).ToList();
                range[graphId] = pathfindingRange;
            }
            return pathfindingRange.AsReadOnly();
        }

        public override bool RemoveRange(int graphId)
        {
            bool isDeleted = base.RemoveRange(graphId);
            if (isDeleted)
            {
                range.Remove(graphId);
            }
            return isDeleted;
        }

        public override bool AddRange((int Order, Vertex Vertex)[] vertices, int graphId)
        {
            bool added = base.AddRange(vertices, graphId);
            if (added)
            {
                foreach(var vertex in vertices)
                {
                    range.TryGetOrAddNew(graphId)
                        .Insert(vertex.Order, vertex.Vertex.Position);
                }
            }
            return added;
        }

        public override bool RemoveRange(IEnumerable<Vertex> vertices, int graphId)
        {
            bool removed = base.RemoveRange(vertices, graphId);
            if (removed)
            {
                foreach (var vertex in vertices)
                {
                    range.TryGetOrAddNew(graphId).Remove(vertex.Position);
                }
            }
            return removed;
        }

        public override bool UpdateRange((int Order, Vertex Vertex)[] vertices, int graphId)
        {
            bool updated = base.UpdateRange(vertices, graphId);
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

        public override bool UpdateObstaclesCount(int newCount, int graphId)
        {
            graphEntities[graphId].ObstaclesCount = newCount;
            return base.UpdateObstaclesCount(newCount, graphId);
        }
    }
}
