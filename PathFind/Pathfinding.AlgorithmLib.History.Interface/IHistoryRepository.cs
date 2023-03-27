using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;
using System;

namespace Pathfinding.AlgorithmLib.History.Interface
{
    public interface IHistoryRepository<out TVolume>
        where TVolume : IHistoryVolume<ICoordinate>
    {
        IEnumerable<ICoordinate> GetObstacles(Guid key);

        IEnumerable<ICoordinate> GetPathfindingRange(Guid key);

        IEnumerable<ICoordinate> GetPath(Guid key);

        IEnumerable<ICoordinate> GetRegulars(Guid key);

        IEnumerable<ICoordinate> GetVisited(Guid key);

        void AddPath(Guid key, ICoordinate path);

        void AddPathfindingRange(Guid key, ICoordinate range);

        void AddVisited(Guid key, ICoordinate visited);

        void AddObstacles(Guid key, ICoordinate obstacles);

        void Remove(Guid key);

        void Clear();
    }
}