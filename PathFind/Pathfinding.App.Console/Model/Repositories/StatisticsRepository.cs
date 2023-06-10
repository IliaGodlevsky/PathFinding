using Pathfinding.App.Console.Model.Notes;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model.Repositories
{
    internal sealed class StatisticsRepository : IRepository<StatisticsNote>
    {
        private readonly Dictionary<Guid, StatisticsNote> notes = new();

        public bool Add(Guid id, StatisticsNote item)
        {
            notes.Add(id, item);
            return true;
        }

        public StatisticsNote Get(Guid id)
        {
            return notes.GetOrDefault(id, () => null);
        }

        public bool Remove(Guid id)
        {
            return notes.Remove(id);
        }

        public void RemoveAll()
        {
            notes.Clear();
        }
    }
}
