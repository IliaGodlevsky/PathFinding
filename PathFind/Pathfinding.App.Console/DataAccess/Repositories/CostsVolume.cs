using Pathfinding.App.Console.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Repositories
{
    internal sealed class CostsVolume : IHistoryVolume<Guid, IReadOnlyList<int>>
    {
        private readonly Dictionary<Guid, IReadOnlyList<int>> costs = new();

        public bool Add(Guid id, IReadOnlyList<int> item)
        {
            costs.Add(id, item);
            return true;
        }

        public IReadOnlyList<int> Get(Guid id)
        {
            return costs.GetOrDefault(id, Array.Empty<int>());
        }

        public bool Remove(Guid id)
        {
            return costs.Remove(id);
        }

        public void RemoveAll()
        {
            costs.Clear();
        }
    }
}
