using Pathfinding.App.Console.DataAccess.ReadDto;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Repo
{
    internal interface IDbContextService
    {
        int AddGraph(IGraph<Vertex> graph);

        IGraph<Vertex> GetGraph(int graphId);

        void AddRangeVertex(Vertex vertex, int order);

        void RemoveRangeVertex(Vertex vertex);

        void AddNeighbour(Vertex vertex, Vertex neighbour);

        void DeleteNeighbour(Vertex vertex, Vertex neighbour);

        IReadOnlyCollection<ICoordinate> GetPathfindingRange(int graphId);

        void UpdateVertexCost(Vertex vertex);

        void UpdateVertexObstacleState(Vertex vertex);

        IReadOnlyCollection<GraphReadDto> GetAllGraphsInfo();

        IReadOnlyCollection<IGraph<Vertex>> GetAllGraphs();

        IReadOnlyCollection<AlgorithmReadDto> GetAllAlgorithmsInfo(int graphId);

        void AddAlgorithm(AlgorithmCreateDto algorithm);
    }
}
