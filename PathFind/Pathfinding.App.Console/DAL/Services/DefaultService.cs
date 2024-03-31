using AutoMapper;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Create;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Services
{
    internal sealed class DefaultService<T>(IMapper mapper,
        IPathfindingRangeBuilder<Vertex> rangeBuilder) : IService<T>
        where T : IVertex
    {
        private readonly IMapper mapper = mapper;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder = rangeBuilder;

        private List<AlgorithmRunHistoryReadDto> runHistory = new();
        private GraphReadDto<T> current = GraphReadDto<T>.Empty;

        public GraphReadDto<T> AddGraph(GraphSerializationDto graph)
        {
            return AddGraph(mapper.Map<GraphCreateDto<T>>(graph));
        }

        public GraphReadDto<T> AddGraph(GraphCreateDto<T> graph)
        {
            current = new() 
            { 
                Id = graph.Graph.GetHashCode(), 
                Name = graph.Name, 
                Graph = graph.Graph 
            };
            return current;
        }

        public bool AddNeighbors(IReadOnlyDictionary<T, IReadOnlyCollection<T>> neighborhoods)
        {
            return false;
        }

        public IReadOnlyCollection<PathfindingHistoryReadDto<T>> AddPathfindingHistory(IEnumerable<PathfindingHistoryCreateDto<T>> histories)
        {
            var history = histories.First();
            current = new()
            {
                Id = history.Graph.Graph.GetHashCode(),
                Name = history.Graph.Name,
                Graph = history.Graph.Graph
            };
            var dto = mapper.Map<IEnumerable<AlgorithmRunHistoryReadDto>>(history.Algorithms);
            runHistory = dto.ForEach((x, i) =>
            {
                x.Statistics.AlgorithmRunId = i;
                x.Statistics.AlgorithmId = x.Run.AlgorithmId;
            }).ToList();

            return new[]
            { 
                new PathfindingHistoryReadDto<T>()
                {
                    Graph = current,
                    Range = mapper.Map<ICoordinate[]>(history.Range),
                    Algorithms = runHistory
                }
            };
        }

        public IReadOnlyCollection<PathfindingHistoryReadDto<T>> AddPathfindingHistory(IEnumerable<PathfindingHistorySerializationDto> histories)
        {
            var read = mapper.Map<IEnumerable<PathfindingHistoryCreateDto<T>>>(histories);
            return AddPathfindingHistory(read);
        }

        public bool AddRange(IEnumerable<(int Order, T Vertex)> vertices, int graphId)
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

        public IReadOnlyList<GraphInformationReadDto> GetAllGraphInfo()
        {
            if (current == GraphReadDto<T>.Empty)
            {
                return Array.Empty<GraphInformationReadDto>();
            }
            return new[]
            {
                new GraphInformationReadDto()
                {
                    Name = current.Name,
                    Id = current.Id,
                    Dimensions = current.Graph.DimensionsSizes,
                    ObstaclesCount = current.Graph.GetObstaclesCount()
                }
            };
        }

        public GraphReadDto<T> GetGraph(int id)
        {
            return current;
        }

        public int GetGraphCount()
        {
            return current != GraphReadDto<T>.Empty ? 1 : 0;
        }

        public IReadOnlyCollection<int> GetGraphIds()
        {
            return new[] { current.GetHashCode() };
        }

        public PathfindingHistoryReadDto<T> GetPathfindingHistory(int graphId)
        {
            return new()
            {
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
                    Costs = current.Graph.Select(x => x.Cost.CurrentCost).ToReadOnly(),
                    Obstacles = current.Graph.GetObstaclesCoordinates().ToReadOnly()
                }
            };
        }

        public IReadOnlyCollection<RunStatisticsDto> GetRunStatisticsForGraph(int graphId)
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

        public bool RemoveNeighbors(IReadOnlyDictionary<T, IReadOnlyCollection<T>> neighborhoods)
        {
            return false;
        }

        public bool RemoveRange(IEnumerable<T> vertices, int graphId)
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

        public bool UpdateRange(IEnumerable<(int Order, T Vertex)> vertices, int graphId)
        {
            return false;
        }

        public bool UpdateVertices(IEnumerable<T> vertices, int graphId)
        {
            return false;
        }
    }
}
