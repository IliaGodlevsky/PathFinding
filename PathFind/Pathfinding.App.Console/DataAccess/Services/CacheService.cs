using AutoMapper;
using LiteDB;
using Pathfinding.App.Console.DataAccess.Dto;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.DataAccess.Mappers;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Services
{
    internal sealed class CacheService : IService
    {
        private readonly IService service;
        private readonly IMapper mapper;

        private bool areAllGraphsFetched = false;
        private readonly Dictionary<int, bool> areAllAlgorithmsFetched = new();

        private readonly Dictionary<int, IGraph<Vertex>> graphs = new();
        private readonly Dictionary<int, List<AlgorithmReadDto>> algorithms = new();
        private readonly Dictionary<int, List<ICoordinate>> range = new();
        private readonly Dictionary<int, GraphEntity> graphEntities = new();

        public CacheService(IMapper mapper, IService service = null)
        {
            this.service = service;
            this.mapper = mapper;
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

        public bool AddNeighbor(Vertex vertex, Vertex neighbor)
        {
            return service.AddNeighbor(vertex, neighbor);
        }

        public int AddPathfindingHistory(PathfindingHistoryCreateDto history)
        {
            int graphId = AddGraph(history.Graph);
            history.Algorithms.ForEach(x =>
            {
                x.GraphId = graphId;
                AddAlgorithm(x);
            });
            int i = 0;
            foreach (var range in history.Range)
            {
                var vertex = history.Graph.Get(range);
                AddRange(vertex, i, graphId);
                i++;
            }
            return graphId;
        }

        public bool AddRange(Vertex vertex, int order, int graphId)
        {
            bool added = service.AddRange(vertex, order, graphId);
            if (added)
            {
                range.TryGetOrAddNew(graphId).Insert(order, vertex.Position);
            }
            return added;
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
            if (areAllGraphsFetched)
            {
                return Array.AsReadOnly(graphEntities.Values.ToArray());
            }
            var entities = Array.AsReadOnly(service.GetAllGraphInfo().ToArray());
            var toSave = entities.ToDictionary(x => x.Id);
            graphEntities.AddRange(toSave);
            areAllGraphsFetched = true;
            return entities;
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
            return Array.AsReadOnly(GetAllGraphInfo().Select(x => x.Id).ToArray());
        }

        public IReadOnlyCollection<AlgorithmReadDto> GetGraphPathfindingHistory(int graphId)
        {
            if (areAllAlgorithmsFetched.ContainsKey(graphId))
            {
                return algorithms.GetOrEmpty(graphId);
            }
            var history = service.GetGraphPathfindingHistory(graphId);
            areAllAlgorithmsFetched[graphId] = true;
            algorithms[graphId] = history.ToList();
            return history;
        }

        public PathfindingHistoryReadDto GetPathfindingHistory(int graphId)
        {
            var graph = GetGraph(graphId);
            var algorithms = GetGraphPathfindingHistory(graphId);
            var range = GetRange(graphId);
            return new PathfindingHistoryReadDto()
            {
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

        public bool RemoveNeighbor(Vertex vertex, Vertex neighbor)
        {
            return service.RemoveNeighbor(vertex, neighbor);
        }

        public bool RemoveRange(Vertex vertex, int graphId)
        {
            bool isDeleted = service.RemoveRange(vertex, graphId);
            if(isDeleted)
            {
                range.TryGetOrAddNew(graphId).Remove(vertex.Position);
            }
            return isDeleted;
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

        public bool UpdateRange(Vertex vertex, int order, int graphId)
        {
            bool updated = service.UpdateRange(vertex, order, graphId);
            if (updated)
            {
                range.TryGetOrAddNew(graphId).Remove(vertex.Position);
                range.TryGetOrAddNew(graphId).Insert(order, vertex.Position);
            }
            return updated;
        }

        public bool UpdateVertex(Vertex vertex, int graphId)
        {
            return service.UpdateVertex(vertex, graphId);
        }

        public bool UpdateVertices(IEnumerable<Vertex> vertices, int graphId)
        {
            return service.UpdateVertices(vertices, graphId);
        }

        public bool UpdateObstaclesCount(int newCount, int graphId)
        {
            graphEntities[graphId].ObstaclesCount = newCount;
            return service.UpdateObstaclesCount(newCount, graphId);
        }
    }
}
