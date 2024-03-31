using Pathfinding.App.Console.DAL.Models.TransferObjects.Create;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface IService<T>
        where T : IVertex
    {
        int GetRunCount(int graphId);

        int GetGraphCount();

        GraphReadDto<T> GetGraph(int id);

        IReadOnlyCollection<int> GetGraphIds();

        IReadOnlyCollection<RunStatisticsDto> GetRunStatisticsForGraph(int graphId);

        RunVisualizationDto GetRunInfo(int runId);

        IReadOnlyList<GraphInformationReadDto> GetAllGraphInfo();

        PathfindingHistoryReadDto<T> GetPathfindingHistory(int graphId);

        IReadOnlyCollection<ICoordinate> GetRange(int graphId);

        bool UpdateObstaclesCount(int newCount, int graphId);

        IReadOnlyCollection<PathfindingHistoryReadDto<T>> AddPathfindingHistory(IEnumerable<PathfindingHistoryCreateDto<T>> histories);

        IReadOnlyCollection<PathfindingHistoryReadDto<T>> AddPathfindingHistory(IEnumerable<PathfindingHistorySerializationDto> histories);

        PathfindingHistorySerializationDto GetSerializationHistory(int graphId);

        GraphSerializationDto GetSerializationGraph(int graphId);

        GraphReadDto<T> AddGraph(GraphSerializationDto graph);

        GraphReadDto<T> AddGraph(GraphCreateDto<T> graph);

        void AddRunHistory(params AlgorithmRunHistoryCreateDto[] histories);

        bool AddNeighbors(IReadOnlyDictionary<T, IReadOnlyCollection<T>> neighborhoods);

        bool UpdateVertices(IEnumerable<T> vertices, int graphId);

        bool AddRange(IEnumerable<(int Order, T Vertex)> vertices, int graphId);

        bool RemoveNeighbors(IReadOnlyDictionary<T, IReadOnlyCollection<T>> neighborhoods);

        bool UpdateRange(IEnumerable<(int Order, T Vertex)> vertices, int graphId);

        bool RemoveRange(IEnumerable<T> vertices, int graphId);

        bool RemoveRange(int graphId);

        bool DeleteGraph(int graphId); // should be cascade
    }
}
