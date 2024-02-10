using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Create;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface IService
    {
        int GetRunCount(int graphId);

        IGraph<Vertex> GetGraph(int id);

        IReadOnlyCollection<int> GetGraphIds();

        IReadOnlyCollection<RunStatisticsDto> GetRunStatiticsForGraph(int graphId);

        IEnumerable<SubAlgorithmReadDto> Insert(IEnumerable<SubAlgorithmCreateDto> subAlgorithms);

        RunVisualizationDto GetRunInfo(int runId);

        IReadOnlyList<GraphEntity> GetAllGraphInfo();

        IReadOnlyCollection<AlgorithmRunHistoryReadDto> GetGraphPathfindingHistory(int graphId);

        PathfindingHistoryReadDto GetPathfindingHistory(int graphId);

        IReadOnlyCollection<ICoordinate> GetRange(int graphId);

        bool UpdateObstaclesCount(int newCount, int graphId);

        IReadOnlyCollection<PathfindingHistoryReadDto> AddPathfindingHistory(IEnumerable<PathfindingHistoryCreateDto> histories);

        IReadOnlyCollection<PathfindingHistoryReadDto> AddPathfindingHistory(IEnumerable<PathfindingHistorySerializationDto> histories);

        PathfindingHistorySerializationDto GetSerializationHistory(int graphId);

        GraphSerializationDto GetSerializationGraph(int graphId);

        GraphReadDto AddGraph(GraphSerializationDto graph);

        GraphReadDto AddGraph(IGraph<Vertex> graph);

        AlgorithmRunReadDto AddAlgorithm(AlgorithmRunCreateDto algorithm);

        void AddRunHistory(params AlgorithmRunHistoryCreateDto[] histories);

        bool AddNeighbors(IReadOnlyDictionary<Vertex, IReadOnlyCollection<Vertex>> neighborhoods);

        bool UpdateVertices(IEnumerable<Vertex> vertices, int graphId);

        bool AddRange(IEnumerable<(int Order, Vertex Vertex)> vertices, int graphId);

        bool RemoveNeighbors(IReadOnlyDictionary<Vertex, IReadOnlyCollection<Vertex>> neighborhoods);

        bool UpdateRange(IEnumerable<(int Order, Vertex Vertex)> vertices, int graphId);

        bool RemoveRange(IEnumerable<Vertex> vertices, int graphId);

        bool RemoveRange(int graphId);

        bool DeleteGraph(int graphId); // should be cascade
    }
}
