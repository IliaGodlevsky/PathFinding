using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class History : IHistory<Brush>
    {
        private readonly ConcurrentDictionary<Guid, Dictionary<ICoordinate, Brush>> history = new();

        public void Add(Guid key, ICoordinate coordinate, Brush brush)
        {
            history.TryGetOrAddNew(key)[coordinate] = brush;
        }

        public void RemoveAll()
        {
            history.Clear();
        }

        public void Remove(Guid key)
        {
            history.TryRemove(key, out _);
        }

        public IEnumerable<(ICoordinate, Brush)> Get(Guid key)
        {
            return history.GetOrEmpty(key).Select(pair => (pair.Key, pair.Value));
        }
    }
}
