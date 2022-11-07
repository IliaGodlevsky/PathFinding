using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Shared.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GraphLib.Realizations.Neighbourhoods
{
    [DebuggerDisplay("Count = {Neighbours.Count}")]
    public sealed class VonNeumannNeighborhood : INeighborhood
    {
        private readonly ICoordinate selfCoordinate;
        private readonly INeighborhood neighbours;
        private readonly Lazy<IReadOnlyCollection<ICoordinate>> neighbourhood;

        private IReadOnlyCollection<ICoordinate> Neighbours => neighbourhood.Value;

        public int Count => Neighbours.Count;

        public VonNeumannNeighborhood(ICoordinate coordinate)
        {
            neighbourhood = new Lazy<IReadOnlyCollection<ICoordinate>>(DetectNeighborhood);
            selfCoordinate = coordinate;
            neighbours = new MooreNeighborhood(coordinate);
        }

        private IReadOnlyCollection<ICoordinate> DetectNeighborhood()
        {
            return neighbours
                .Where(neighbour => neighbour.IsCardinal(selfCoordinate))
                .ToReadOnly();
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