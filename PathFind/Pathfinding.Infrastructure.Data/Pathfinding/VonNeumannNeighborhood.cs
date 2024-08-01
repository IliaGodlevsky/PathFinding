using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Data.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pathfinding.Infrastructure.Data.Pathfinding
{
    [DebuggerDisplay("Count = {Neighbours.Count}")]
    public sealed class VonNeumannNeighborhood : INeighborhood
    {
        private readonly Lazy<IReadOnlyCollection<ICoordinate>> neighbourhood;

        public int Count => neighbourhood.Value.Count;

        public VonNeumannNeighborhood(ICoordinate coordinate)
        {
            neighbourhood = new Lazy<IReadOnlyCollection<ICoordinate>>(
                () => DetectNeighborhood(coordinate), true);
        }

        private IReadOnlyCollection<ICoordinate> DetectNeighborhood(ICoordinate self)
        {
            var mooreNeighbourhood = new MooreNeighborhood(self);
            return mooreNeighbourhood
                .Where(neighbour => neighbour.IsCardinal(self))
                .ToArray();
        }

        public IEnumerator<ICoordinate> GetEnumerator()
        {
            return neighbourhood.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}