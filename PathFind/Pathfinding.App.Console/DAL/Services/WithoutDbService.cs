using AutoMapper;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Create;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Services
{
    internal sealed class WithoutDbService(IMapper mapper,
        IPathfindingRangeBuilder<Vertex> rangeBuilder) : IService
    {
        private readonly IMapper mapper = mapper;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder = rangeBuilder;

        private List<AlgorithmRunHistoryReadDto> runHistory = new();
        private IGraph<Vertex> current = Graph<Vertex>.Empty;

        public GraphReadDto AddGraph(GraphSerializationDto graph)
        {
            return AddGraph(mapper.Map<IGraph<Vertex>>(graph));
        }

        public GraphReadDto AddGraph(IGraph<Vertex> graph)
        {
            current = graph;
            return new() { Graph = graph, Id = current.GetHashCode() };
        }

        public bool AddNeighbors(IReadOnlyDictionary<Vertex, IReadOnlyCollection<Vertex>> neighborhoods)
        {
            return false;
        }

        public IReadOnlyCollection<PathfindingHistoryReadDto> AddPathfindingHistory(IEnumerable<PathfindingHistoryCreateDto> histories)
        {
            var history = histories.First();
            current = mapper.Map<IGraph<Vertex>>(history.Graph);
            runHistory = new(mapper.Map<IEnumerable<AlgorithmRunHistoryReadDto>>(history.Algorithms));
            for (int i = 0; i < runHistory.Count; i++)
            {
                runHistory[i].Statistics.AlgorithmRunId = i;
                runHistory[i].Statistics.AlgorithmId = runHistory[i].Run.AlgorithmId;
            }
            return new PathfindingHistoryReadDto()
            {
                Id = current.GetHashCode(),
                Graph = current,
                Range = mapper.Map<ICoordinate[]>(history.Range),
                Algorithms = runHistory
            }.Enumerate().ToReadOnly();
        }

        public IReadOnlyCollection<PathfindingHistoryReadDto> AddPathfindingHistory(IEnumerable<PathfindingHistorySerializationDto> histories)
        {
            var read = mapper.Map<IEnumerable<PathfindingHistoryCreateDto>>(histories);
            return AddPathfindingHistory(read);
        }

        public bool AddRange(IEnumerable<(int Order, Vertex Vertex)> vertices, int graphId)
        {
            return false;
        }

        public void AddRunHistory(params AlgorithmRunHistoryCreateDto[] histories)
        {
            runHistory.AddRange(mapper.Map<IEnumerable<AlgorithmRunHistoryReadDto>>(histories));
            for (int i = 0; i < runHistory.Count; i++)
            {
                runHistory[i].Statistics.AlgorithmRunId = i;
            }
        }

        public bool DeleteGraph(int graphId)
        {
            return false;
        }

        public IReadOnlyList<GraphEntity> GetAllGraphInfo()
        {
            if (current == Graph<Vertex>.Empty)
            {
                return Array.Empty<GraphEntity>();
            }
            return new[]
            {
                new GraphEntity()
                {
                    Id = current.GetHashCode(),
                    Width = current.GetWidth(),
                    Length = current.GetLength(),
                    ObstaclesCount = current.GetObstaclesCount()
                }
            };
        }

        public IGraph<Vertex> GetGraph(int id)
        {
            return current;
        }

        public int GetGraphCount()
        {
            return current != Graph<Vertex>.Empty ? 1 : 0;
        }

        public IReadOnlyCollection<int> GetGraphIds()
        {
            return new[] { 0 };
        }

        public PathfindingHistoryReadDto GetPathfindingHistory(int graphId)
        {
            return new()
            {
                Id = graphId,
                Graph = current,
                Algorithms = runHistory,
                Range = rangeBuilder.Range.GetCoordinates().ToReadOnly()
            };
        }

        public IReadOnlyCollection<ICoordinate> GetRange(int graphId)
        {
            return rangeBuilder.Range.GetCoordinates().ToReadOnly();
        }

        public int GetRunCount(int graphId)
        {
            return runHistory.Count;
        }

        public RunVisualizationDto GetRunInfo(int runId)
        {
            return new()
            {
                AlgorithmSpeed = runHistory[runId].Statistics.AlgorithmSpeed,
                Algorithms = runHistory[runId].SubAlgorithms,
                GraphState = new()
                {
                    AlgorithmRunId = runId,
                    Range = rangeBuilder.Range.GetCoordinates().ToReadOnly(),
                    Costs = current.GetCosts(),
                    Obstacles = current.GetObstaclesCoordinates().ToReadOnly()
                }
            };
        }

        public IReadOnlyCollection<RunStatisticsDto> GetRunStatiticsForGraph(int graphId)
        {
            return runHistory.Select(x => x.Statistics).ToReadOnly();
        }

        public GraphSerializationDto GetSerializationGraph(int graphId)
        {
            return mapper.Map<GraphSerializationDto>(current);
        }

        public PathfindingHistorySerializationDto GetSerializationHistory(int graphId)
        {
            return new()
            {
                Graph = mapper.Map<GraphSerializationDto>(current),
                Algorithms = mapper.Map<AlgorithmRunHistorySerializationDto[]>(runHistory),
                Range = mapper.Map<CoordinateDto[]>(rangeBuilder.Range.GetCoordinates())
            };
        }

        public bool RemoveNeighbors(IReadOnlyDictionary<Vertex, IReadOnlyCollection<Vertex>> neighborhoods)
        {
            return false;
        }

        public bool RemoveRange(IEnumerable<Vertex> vertices, int graphId)
        {
            return false;
        }

        public bool RemoveRange(int graphId)
        {
            return false;
        }

        public bool UpdateObstaclesCount(int newCount, int graphId)
        {
            return false;
        }

        public bool UpdateRange(IEnumerable<(int Order, Vertex Vertex)> vertices, int graphId)
        {
            return false;
        }

        public bool UpdateVertices(IEnumerable<Vertex> vertices, int graphId)
        {
            return false;
        }
    }
}
