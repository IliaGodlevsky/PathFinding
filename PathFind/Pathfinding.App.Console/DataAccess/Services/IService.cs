using Pathfinding.App.Console.DataAccess.Dto;
using Pathfinding.App.Console.DataAccess.Entities;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Services
{
    internal interface IService
    {
        IGraph<Vertex> GetGraph(int id);

        IReadOnlyCollection<int> GetGraphIds();

        IReadOnlyList<GraphEntity> GetAllGraphInfo();

        IReadOnlyCollection<AlgorithmReadDto> GetGraphPathfindingHistory(int graphId);

        PathfindingHistoryReadDto GetPathfindingHistory(int graphId);

        IReadOnlyCollection<ICoordinate> GetRange(int graphId);

        bool UpdateObstaclesCount(int newCount, int graphId);

        PathfindingHistoryReadDto AddPathfindingHistory(PathfindingHistoryCreateDto history);

        int AddGraph(IGraph<Vertex> graph);

        int AddAlgorithm(AlgorithmCreateDto algorithm);

        bool AddNeighbor(Vertex vertex, Vertex neighbor);

        bool AddRange(Vertex vertex, int order, int graphId);

        bool UpdateVertex(Vertex vertex, int graphId);

        bool UpdateVertices(IEnumerable<Vertex> vertices, int graphId);

        bool UpdateRange(Vertex vertex, int order, int graphId);

        bool RemoveNeighbor(Vertex vertex, Vertex neighbor);

        bool RemoveRange(Vertex vertex, int graphId);

        bool RemoveRange(int graphId);

        bool DeleteGraph(int graphId);
    }
}
