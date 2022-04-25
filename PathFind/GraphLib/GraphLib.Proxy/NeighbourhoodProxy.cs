using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Proxy
{
    internal sealed class NeighbourhoodProxy : INeighborhood
    {
        public IReadOnlyCollection<ICoordinate> Neighbours { get; }

        public NeighbourhoodProxy(IVertex vertex)
        {
            Neighbours = vertex.Neighbours
                .Select(neighbour => neighbour.Position)
                .ToArray();
        }

        internal NeighbourhoodProxy(IReadOnlyCollection<ICoordinate> coordinates)
        {
            Neighbours = coordinates.ToArray();
        }
    }
}
