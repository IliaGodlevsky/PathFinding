using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Services
{
    internal interface IService
    {
        IReadOnlyList<GraphModel> GetAllGraphs();

        IReadOnlyList<AlgorithmModel> GetByGraphId(Guid id);

        IReadOnlyList<CoordinatesModel> GetVisitedByAlgorithm(Guid id);

        IReadOnlyList<CoordinatesModel> GetObstaclesByAlgorithm(Guid id);

        IReadOnlyList<CoordinatesModel> GetPathsByAlgorithm(Guid id);

        IReadOnlyList<CoordinatesModel> GetRangedByAlgorithm(Guid id);

        IReadOnlyList<CostsModel> GetCostsByAlgorithm(Guid id);

        StatisticsModel GetStatisticsByAlgorithm(Guid id);

        GraphModel AddGraph(Graph2D<Vertex> graph);

        GraphModel UpdateGraph(Guid id, Graph2D<Vertex> graph);

        GraphModel UpdateGraph(Guid id, IEnumerable<Vertex> range);

        AlgorithmModel AddAlgorithm(PathfindingProcess process, Guid graphId);

        void AddPathfindingRange(Guid algorithmId, IReadOnlyList<ICoordinate> range);

        void AddPath(Guid algorithmId, IReadOnlyList<ICoordinate> path);

        void AddObstcles(Guid algorithmId, IReadOnlyList<ICoordinate> obstacles);

        void AddVisited(Guid algorithmId, IReadOnlyList<ICoordinate> visited);

        void AddStatistics(Guid algorithmId, Statistics statistics);

        void AddCosts(Guid algorithmId, IReadOnlyList<int> costs);
    }
}
