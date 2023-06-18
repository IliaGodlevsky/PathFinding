using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.Notes;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Repositories
{
    internal sealed class StatisticsVolume : IHistoryVolume<Guid, Statistics>
    {
        private readonly Dictionary<Guid, Statistics> notes = new();

        public bool Add(Guid id, Statistics item)
        {
            notes.Add(id, item);
            return true;
        }

        public Statistics Get(Guid id)
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
