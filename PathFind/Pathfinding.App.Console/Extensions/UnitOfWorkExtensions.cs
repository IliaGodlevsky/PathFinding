using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.DataAccess.UnitOfWorks;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class UnitOfWorkExtensions
    {
        public static CostsModel AddCosts(this IUnitOfWork unitOfWork, long algorithmId, IEnumerable<int> costs)
        {
            var model = new CostsModel
            {
                AlgorithmId = algorithmId,
                Costs = costs.ToArray()
            };
            return unitOfWork.CostsRepository.Add(model);
        }

        public static CoordinatesModel AddObstacles(this IUnitOfWork unitOfWork, long algorithmId, IEnumerable<ICoordinate> obstacles)
        {
            var model = Create(algorithmId, obstacles);
            return unitOfWork.ObstaclesRepository.Add(model);
        }

        public static CoordinatesModel AddPath(this IUnitOfWork unitOfWork, long algorithmId, IEnumerable<ICoordinate> path)
        {
            var model = Create(algorithmId, path);
            return unitOfWork.PathsRepository.Add(model);
        }

        public static CoordinatesModel AddRange(this IUnitOfWork unitOfWork, long algorithmId, IEnumerable<ICoordinate> range)
        {
            var model = Create(algorithmId, range);
            return unitOfWork.RangesRepository.Add(model);
        }

        public static CoordinatesModel AddVisited(this IUnitOfWork unitOfWork, long algorithmId, IEnumerable<ICoordinate> visited)
        {
            var model = Create(algorithmId, visited);
            return unitOfWork.VisitedRepository.Add(model);
        }

        public static AlgorithmModel AddAlgorithm(this IUnitOfWork unitOfWork, long graphId, string algorithmName)
        {
            var model = new AlgorithmModel
            {
                GraphId = graphId,
                Name = algorithmName
            };
            return unitOfWork.AlgorithmRepository.Add(model);
        }

        public static GraphModel AddGraph(this IUnitOfWork unitOfWork, Graph2D<Vertex> graph)
        {
            var model = new GraphModel()
            {
                Range = Array.Empty<ICoordinate>(),
                Graph = graph
            };
            return unitOfWork.GraphRepository.Add(model);
        }

        public static StatisticsModel AddStatistics(this IUnitOfWork unitOfWork, long algorithmId, StatisticsModel statistics)
        {
            statistics.AlgorithmId = algorithmId;
            return unitOfWork.StatisticsRepository.Add(statistics);
        }

        public static GraphInformationModel AddGraphInformation(this IUnitOfWork unitOfWork, long graphId, string info)
        {
            var model = new GraphInformationModel
            {
                Description = info,
                GraphId = graphId,
            };
            return unitOfWork.InformationRepository.Add(model);
        }
            

        public static GraphModel UpdateGraph(this IUnitOfWork unitOfWork, GraphModel graph)
        {
            return unitOfWork.GraphRepository.Update(graph);
        }

        private static CoordinatesModel Create(long algorithmId, IEnumerable<ICoordinate> coordinates)
        {
            var model = new CoordinatesModel
            {
                AlgorithmId = algorithmId,
                Coordinates = coordinates.ToArray()
            };
            return model;
        }
    }
}
