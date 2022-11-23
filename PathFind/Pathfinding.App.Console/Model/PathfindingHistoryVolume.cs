using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model
{
    internal sealed class PathfindingHistoryVolume : IHistoryVolume<ICoordinate>
    {
        private readonly Dictionary<Guid, List<ICoordinate>> history;

        public PathfindingHistoryVolume()
        {
            history = new Dictionary<Guid, List<ICoordinate>>();
        }

        public void Add(Guid key, ICoordinate item)
        {
            history.TryGetOrAddNew(key).Add(item);
        }

        public void RemoveAll()
        {
            history.Clear();
        }

        public void Remove(Guid key)
        {
            history.Remove(key);
        }

        public IEnumerable<ICoordinate> Get(Guid key)
        {
            return history.GetOrEmpty(key);
        }
    }
}
