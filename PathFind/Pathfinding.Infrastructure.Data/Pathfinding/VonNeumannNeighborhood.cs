using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Shared.Primitives;
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
        private readonly Lazy<IReadOnlyCollection<Coordinate>> neighbourhood;

        public int Count => neighbourhood.Value.Count;

        public VonNeumannNeighborhood(Coordinate coordinate)
        {
            neighbourhood = new Lazy<IReadOnlyCollection<Coordinate>>(
                () => DetectNeighborhood(coordinate), true);
        }

        private IReadOnlyCollection<Coordinate> DetectNeighborhood(Coordinate self)
        {
            var mooreNeighbourhood = new MooreNeighborhood(self);
            return mooreNeighbourhood
                .Where(neighbour => neighbour.IsCardinal(self))
                .ToArray();
        }

        public IEnumerator<Coordinate> GetEnumerator()
        {
            return neighbourhood.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}