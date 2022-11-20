using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.AlgorithmLib.History
{
    public sealed class History<TVolume> 
        where TVolume : IHistoryVolume<ICoordinate>, new()
    {
        private const int VolumesCount = 4;
        private const int RangeHistory = 0;
        private const int PathHistory = 1;
        private const int VisitedHistory = 2;
        private const int ObstaclesHistory = 3;

        private readonly TVolume[] volumes;

        public History()
        {
            volumes = VolumesCount.Times<TVolume>().ToArray();
        }

        public IEnumerable<ICoordinate> GetObstacles(Guid key)
        {
            return volumes[ObstaclesHistory].Get(key);
        }

        public IEnumerable<ICoordinate> GetPathfindingRange(Guid key)
        {
            return volumes[RangeHistory].Get(key);
        }

        public IEnumerable<ICoordinate> GetPath(Guid key)
        {
            return volumes[PathHistory].Get(key);
        }

        public IEnumerable<ICoordinate> GetVisitedVertices(Guid key)
        {
            return volumes[VisitedHistory].Get(key);
        }

        public void AddPath(Guid key, IGraphPath path)
        {
            path.ForEach(item => volumes[PathHistory].Add(key, item));
        }

        public void AddPathfindingRange(Guid key, IPathfindingRange range)
        {
            range.ForEach(item => volumes[RangeHistory].Add(key, item.Position));
        }

        public void AddVisited(Guid key, ICoordinate visited)
        {
            volumes[VisitedHistory].Add(key, visited);
        }

        public void AddObstacles(Guid key, IEnumerable<ICoordinate> obstacles)
        {
            obstacles.ForEach(item => volumes[ObstaclesHistory].Add(key, item));
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
