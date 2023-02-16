using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Collections;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.History
{
    public sealed class History<TVolume>
        where TVolume : IHistoryVolume<ICoordinate>, new()
    {
        private readonly int volumesCount = 5;
        private readonly ReadOnlyList<TVolume> volumes;

        private TVolume RangeHistory => volumes[0];

        private TVolume PathHistory => volumes[1];

        private TVolume VisitedHistory => volumes[2];

        private TVolume ObstaclesHistory => volumes[3];

        private TVolume RegularHistory => volumes[4];

        public History()
        {
            volumes = volumesCount.Times<TVolume>().ToReadOnly();
        }

        public IEnumerable<ICoordinate> GetObstacles(Guid key)
        {
            return ObstaclesHistory.Get(key);
        }

        public IEnumerable<ICoordinate> GetPathfindingRange(Guid key)
        {
            return RangeHistory.Get(key);
        }

        public IEnumerable<ICoordinate> GetPath(Guid key)
        {
            return PathHistory.Get(key);
        }

        public IEnumerable<ICoordinate> GetRegulars(Guid key)
        {
            return RegularHistory.Get(key);
        }

        public IEnumerable<ICoordinate> GetVisitedVertices(Guid key)
        {
            return VisitedHistory.Get(key);
        }

        public void AddPath(Guid key, IEnumerable<ICoordinate> path)
        {
            path.ForEach(item => PathHistory.Add(key, item));
        }

        public void AddPathfindingRange(Guid key, IEnumerable<IVertex> range)
        {
            range.ForEach(item => RangeHistory.Add(key, item.Position));
        }

        public void AddVisited(Guid key, ICoordinate visited)
        {
            VisitedHistory.Add(key, visited);
        }

        public void AddObstacles(Guid key, IEnumerable<ICoordinate> obstacles)
        {
            obstacles.ForEach(item => ObstaclesHistory.Add(key, item));
        }

        public void Remove(Guid key)
        {
            volumes.ForEach(volume => volume.Remove(key));
        }

        public void Clear()
        {
            volumes.ForEach(volume => volume.RemoveAll());
        }
    }
}
