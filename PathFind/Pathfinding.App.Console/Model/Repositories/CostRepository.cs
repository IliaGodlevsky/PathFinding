using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model.Repositories
{
    internal sealed class CostRepository : IRepository<IReadOnlyList<int>>
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
