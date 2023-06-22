using Pathfinding.AlgorithmLib.History;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.DataAccess.UnitOfWorks;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Serialization;
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

        private static GraphModel AddGraph(this IUnitOfWork unitOfWork, GraphModel model)
        {
            return unitOfWork.GraphRepository.Add(model);
        }
            

        public static GraphModel UpdateGraph(this IUnitOfWork unitOfWork, GraphModel graph)
        {
            return unitOfWork.GraphRepository.Update(graph);
        }

        public static SerializationInfo GetSerializationInfo(this IUnitOfWork unitOfWork, long graphId)
        {
            var info = new SerializationInfo();
            var graph = unitOfWork.GraphRepository.GetById(graphId);
            info.Graph = graph.Graph;
            info.Range = graph.Range.ToArray();
            info.GraphInformation = info.Graph.ToString();
            var algorithms = unitOfWork.AlgorithmRepository
                    .GetAll(a => a.GraphId == graph.Id)
                    .ToArray();
            foreach (var algorithm in algorithms)
            {
                info.Algorithms.Add(algorithm.Name);
                var visited = unitOfWork.VisitedRepository
                    .GetBy(c => c.AlgorithmId == algorithm.Id)
                    .Coordinates;
                info.Visited.Add(visited);
                var obstacles = unitOfWork.ObstaclesRepository
                    .GetBy(c => c.AlgorithmId == algorithm.Id)
                    .Coordinates;
                info.Obstacles.Add(obstacles);
                var range = unitOfWork.RangesRepository
                    .GetBy(c => c.AlgorithmId == algorithm.Id)
                    .Coordinates;
                info.Ranges.Add(range);
                var path = unitOfWork.PathsRepository
                    .GetBy(c => c.AlgorithmId == algorithm.Id)
                    .Coordinates;
                info.Paths.Add(path);
                var costs = unitOfWork.CostsRepository
                    .GetBy(c => c.AlgorithmId == algorithm.Id)
                    .Costs;
                info.Costs.Add(costs);
                var statistics = unitOfWork.StatisticsRepository
                    .GetBy(s => s.AlgorithmId == algorithm.Id);
                info.Statistics.Add(statistics);
            }
            return info;
        }

        public static void AddSerializationInfo(this IUnitOfWork unitOfWork, SerializationInfo info)
        {
            var model = new GraphModel()
            {
                Graph = info.Graph,
                Range = info.Range
            };
            model = unitOfWork.AddGraph(model);
            unitOfWork.AddGraphInformation(model.Id, model.Range.ToString());
            for (int i = 0; i < info.Algorithms.Count; i++)
            {
                var algorithm = unitOfWork.AddAlgorithm(model.Id, info.Algorithms[i]);
                unitOfWork.AddVisited(algorithm.Id, info.Visited[i]);
                unitOfWork.AddObstacles(algorithm.Id, info.Obstacles[i]);
                unitOfWork.AddPath(algorithm.Id, info.Paths[i]);
                unitOfWork.AddRange(algorithm.Id, info.Ranges[i]);
                unitOfWork.AddCosts(algorithm.Id, info.Costs[i]);
                unitOfWork.AddStatistics(algorithm.Id, info.Statistics[i]);
            }
        }

        public static VisualizationSet GetVisualizationSet(this IUnitOfWork unitOfWork, long algorithmId)
        {
            var set = new VisualizationSet();
            set.Visited = unitOfWork.VisitedRepository.GetBy(v => v.AlgorithmId == algorithmId).Coordinates;
            set.Obstacles = unitOfWork.ObstaclesRepository.GetBy(o => o.AlgorithmId == algorithmId).Coordinates;
            set.Range = unitOfWork.RangesRepository.GetBy(r => r.AlgorithmId == algorithmId).Coordinates;
            set.Path = unitOfWork.PathsRepository.GetBy(p => p.AlgorithmId == algorithmId).Coordinates;
            set.Costs = unitOfWork.CostsRepository.GetBy(c => c.AlgorithmId == algorithmId).Costs;
            set.Statistics = unitOfWork.StatisticsRepository.GetBy(s => s.AlgorithmId == algorithmId);
            return set;
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
