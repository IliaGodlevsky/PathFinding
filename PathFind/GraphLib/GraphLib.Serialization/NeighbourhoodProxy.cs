using Common.Interface;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Serialization
{
    [Serializable]
    internal sealed class NeighbourhoodProxy : INeighborhood, ICloneable<INeighborhood>
    {
        public IReadOnlyCollection<ICoordinate> Neighbours { get; }

        public NeighbourhoodProxy(IVertex vertex)
        {
            Neighbours = vertex.Neighbours
                .Select(neighbour => neighbour.Position)
                .ToArray();
        }

        private NeighbourhoodProxy(IReadOnlyCollection<ICoordinate> coordinates)
        {
            Neighbours = coordinates.ToArray();
        }

        public INeighborhood Clone()
        {
            return new NeighbourhoodProxy(Neighbours);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
