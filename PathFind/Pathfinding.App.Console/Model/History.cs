using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model
{
    internal sealed class History : IHistory<ConsoleColor>
    {
        private readonly Dictionary<Guid, List<(ICoordinate, ConsoleColor)>> history = new();

        public void Add(Guid key, ICoordinate coordinate, ConsoleColor color)
        {
            history.TryGetOrAddNew(key).Add((coordinate, color));
        }

        public void RemoveAll()
        {
            history.Clear();
        }

        public void Remove(Guid key)
        {
            history.Remove(key);
        }

        public IEnumerable<(ICoordinate, ConsoleColor)> Get(Guid key)
        {
            return history.GetOrEmpty(key);
        }
    }
}
