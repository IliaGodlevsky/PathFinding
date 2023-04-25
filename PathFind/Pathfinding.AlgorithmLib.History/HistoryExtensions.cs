using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.History
{
    public static class HistoryExtensions
    {
        public static void AddPath(this IHistoryRepository<IHistoryVolume<ICoordinate>> repository,
            Guid key, IEnumerable<ICoordinate> vertices)
        {
            foreach (var vertex in vertices)
            {
                repository.AddPath(key, vertex);
            }
        }

        public static void AddPathfindingRange(this IHistoryRepository<IHistoryVolume<ICoordinate>> repository,
            Guid key, IEnumerable<IVertex> vertices)
        {
            foreach (var vertex in vertices)
            {
                repository.AddPathfindingRange(key, vertex.Position);
            }
        }

        public static void AddObstacles(this IHistoryRepository<IHistoryVolume<ICoordinate>> repository,
            Guid key, IEnumerable<ICoordinate> vertices)
        {
            foreach (var vertex in vertices)
            {
                repository.AddObstacles(key, vertex);
            }
        }
    }
}
