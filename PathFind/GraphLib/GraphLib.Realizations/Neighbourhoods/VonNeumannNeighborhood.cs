using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GraphLib.Realizations.Neighbourhoods
{
    [DebuggerDisplay("Count = {Neighbours.Length}")]
    public sealed class VonNeumannNeighborhood : INeighborhood
    {
        private readonly ICoordinate selfCoordinate;
        private readonly INeighborhood neighboursCoordinates;
        private readonly Lazy<IReadOnlyCollection<ICoordinate>> neighbourhood;

        public IReadOnlyCollection<ICoordinate> Neighbours => neighbourhood.Value;

        public VonNeumannNeighborhood(ICoordinate coordinate)
        {
            neighbourhood = new Lazy<IReadOnlyCollection<ICoordinate>>(DetectNeighborhood);
            selfCoordinate = coordinate;
            neighboursCoordinates = new MooreNeighborhood(coordinate);
        }

        public INeighborhood Clone()
        {
            return new VonNeumannNeighborhood(selfCoordinate.Clone());
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        private IReadOnlyCollection<ICoordinate> DetectNeighborhood()
        {
            return neighboursCoordinates.Neighbours
                .Where(neighbour => neighbour.IsCardinal(selfCoordinate))
                .ToArray();
        }
    }
}