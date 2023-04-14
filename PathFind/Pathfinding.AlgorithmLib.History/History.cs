using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.AlgorithmLib.History
{
    public sealed class History<TVolume> : IHistoryRepository<TVolume>
        where TVolume : IHistoryVolume<ICoordinate>, new()
    {
        private readonly int volumesCount = 5;
        private readonly TVolume[] volumes;

        private TVolume RangeHistory => volumes[0];

        private TVolume PathHistory => volumes[1];

        private TVolume VisitedHistory => volumes[2];

        private TVolume ObstaclesHistory => volumes[3];

        private TVolume RegularHistory => volumes[4];

        public History()
        {
            volumes = new TVolume[volumesCount];
            for (int i = 0; i < volumesCount; i++)
            {
                volumes[i] = new TVolume();
            }
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

        public IEnumerable<ICoordinate> GetVisited(Guid key)
        {
            return VisitedHistory.Get(key);
        }

        public void AddPath(Guid key, ICoordinate path)
        {
            PathHistory.Add(key, path);
        }

        public void AddPathfindingRange(Guid key, ICoordinate range)
        {
            RangeHistory.Add(key, range);
        }

        public void AddVisited(Guid key, ICoordinate visited)
        {
            VisitedHistory.Add(key, visited);
        }

        public void AddObstacles(Guid key, ICoordinate obstacle)
        {
            ObstaclesHistory.Add(key, obstacle);
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
