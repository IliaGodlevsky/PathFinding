using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model.Repositories
{
    internal sealed class CoordinatesRepository : IRepository<IReadOnlyList<ICoordinate>>
    {
        private readonly Dictionary<Guid, IReadOnlyList<ICoordinate>> coordinates = new();

        public bool Add(Guid id, IReadOnlyList<ICoordinate> item)
        {
            coordinates.Add(id, item);
            return true;
        }

        public IReadOnlyList<ICoordinate> Get(Guid id)
        {
            return coordinates.GetOrDefault(id, Array.Empty<ICoordinate>());
        }

        public bool Remove(Guid id)
        {
            return coordinates.Remove(id);
        }

        public void RemoveAll()
        {
            coordinates.Clear();
        }
    }
}
