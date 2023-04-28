using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Neighborhoods
{
    [DebuggerDisplay("Count = {Neighbours.Count}")]
    public sealed class VonNeumannNeighborhood : INeighborhood
    {
        private readonly Lazy<IReadOnlyCollection<ICoordinate>> neighbourhood;

        private IReadOnlyCollection<ICoordinate> Neighbours => neighbourhood.Value;

        public int Count => Neighbours.Count;

        public VonNeumannNeighborhood(ICoordinate coordinate)
        {
            neighbourhood = new(() => DetectNeighborhood(coordinate), true);
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
            return Neighbours.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}