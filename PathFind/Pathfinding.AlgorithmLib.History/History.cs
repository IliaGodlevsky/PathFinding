using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.History
{
    public sealed class History<TVolume>
        where TVolume : IHistoryVolume<ICoordinate>, new()
    {
        private readonly TVolume pathfindingRangeHistory;
        private readonly TVolume pathHistory;
        private readonly TVolume visitedHistory;
        private readonly TVolume obstaclesHistory;

        public History()
        {
            pathfindingRangeHistory = new TVolume();
            pathHistory = new TVolume();
            visitedHistory = new TVolume();
            obstaclesHistory = new TVolume();
        }

        public IEnumerable<ICoordinate> GetObstacles(Guid key)
        {
            return obstaclesHistory.Get(key);
        }

        public IEnumerable<ICoordinate> GetPathfindingRange(Guid key)
        {
            return pathfindingRangeHistory.Get(key);
        }

        public IEnumerable<ICoordinate> GetPath(Guid key)
        {
            return pathHistory.Get(key);
        }

        public IEnumerable<ICoordinate> GetVisitedVertices(Guid key)
        {
            return visitedHistory.Get(key);
        }

        public void AddPath(Guid key, IGraphPath path)
        {
            foreach (var item in path)
            {
                pathHistory.Add(key, item);
            }
        }

        public void AddPathfindingRange(Guid key, IPathfindingRange range)
        {
            foreach (var item in range)
            {
                pathfindingRangeHistory.Add(key, item.Position);
            }
        }

        public void AddVisited(Guid key, ICoordinate visited)
        {
            visitedHistory.Add(key, visited);
        }

        public void AddObstacles(Guid key, IEnumerable<ICoordinate> obstacles)
        {
            foreach (var item in obstacles)
            {
                obstaclesHistory.Add(key, item);
            }
        }

        public void Remove(Guid key)
        {
            pathfindingRangeHistory.Remove(key);
            pathHistory.Remove(key);
            visitedHistory.Remove(key);
            obstaclesHistory.Remove(key);
        }

        public void Clear()
        {
            pathfindingRangeHistory.RemoveAll();
            pathHistory.RemoveAll();
            visitedHistory.RemoveAll();
            obstaclesHistory.RemoveAll();
        }
    }
}
