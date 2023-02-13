using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Pathfinding.App.WPF._3D.Model
{
    internal sealed class PathfindingHistoryVolume : IHistory<ICoordinate>
    {
        private readonly ConcurrentDictionary<Guid, ConcurrentBag<ICoordinate>> history = new();

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
            history.TryRemove(key, out _);
        }

        public IEnumerable<ICoordinate> Get(Guid key)
        {
            return history.GetOrEmpty(key);
        }
    }
}
