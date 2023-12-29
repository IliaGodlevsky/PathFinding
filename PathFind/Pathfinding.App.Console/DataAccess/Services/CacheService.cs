using Pathfinding.App.Console.DataAccess.Dto;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Interface;
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
        private readonly Dictionary<int, Dictionary<ICoordinate, List<ICoordinate>>> addedNeighbours = new();
        private readonly Dictionary<int, Dictionary<ICoordinate, List<ICoordinate>>> deletedNeighbours = new();

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
            graphEntities.Add(id, new()
            {
                Id = id,
                Width = graph.GetWidth(),
                Length = graph.GetLength(),
                ObstaclesCount = graph.GetObstaclesCount()
            });
            graphs[id] = graph;
            return id;
        }

        public override PathfindingHistoryReadDto AddPathfindingHistory(PathfindingHistoryCreateDto history)
        {
            var dto = base.AddPathfindingHistory(history);
            graphs[dto.Id] = dto.Graph;
            graphEntities.Add(dto.Id, new()
            {
                Id = dto.Id,
                Width = dto.Graph.GetWidth(),
                Length = dto.Graph.GetLength(),
                ObstaclesCount = dto.Graph.GetObstaclesCount()
            });
            algorithms.TryGetOrAddNew(dto.Id).AddRange(dto.Algorithms);
            range.TryGetOrAddNew(dto.Id).AddRange(history.Range);
            return dto;
        }

        public override bool AddRange(Vertex vertex, int order, int graphId)
        {
            bool added = base.AddRange(vertex, order, graphId);
            if (added)
            {
                range.TryGetOrAddNew(graphId).Insert(order, vertex.Position);
            }
            return added;
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
            if (areAllGraphsFetched)
            {
                return graphEntities.Values.ToList().AsReadOnly();
            }
            var entities = base.GetAllGraphInfo();
            foreach (var value in entities)
            {
                graphEntities[value.Id] = value;
            }
            areAllGraphsFetched = true;
            return entities;
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
            if (areAllAlgorithmsFetched.Contains(graphId))
            {
                return algorithms.GetOrEmpty(graphId);
            }
            var history = base.GetGraphPathfindingHistory(graphId);
            areAllAlgorithmsFetched.Add(graphId);
            algorithms[graphId] = history.ToList();
            return history;
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

        public override bool RemoveRange(Vertex vertex, int graphId)
        {
            bool isDeleted = base.RemoveRange(vertex, graphId);
            if(isDeleted)
            {
                range.TryGetOrAddNew(graphId).Remove(vertex.Position);
            }
            return isDeleted;
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

        public override bool UpdateRange(Vertex vertex, int order, int graphId)
        {
            bool updated = base.UpdateRange(vertex, order, graphId);
            if (updated)
            {
                range.TryGetOrAddNew(graphId).Remove(vertex.Position);
                range.TryGetOrAddNew(graphId).Insert(order, vertex.Position);
            }
            return updated;
        }

        public override bool UpdateObstaclesCount(int newCount, int graphId)
        {
            graphEntities[graphId].ObstaclesCount = newCount;
            return base.UpdateObstaclesCount(newCount, graphId);
        }

        public override bool AddNeighbor(Vertex vertex, Vertex neighbor)
        {
            bool added = base.AddNeighbor(vertex, neighbor);
            if (added)
            {
                
            }
            return added;
        }

        public override bool RemoveNeighbor(Vertex vertex, Vertex neighbor)
        {
            bool isDeleted = base.RemoveNeighbor(vertex, neighbor);
            if (isDeleted)
            {

            }
            return isDeleted;
        }
    }
}
