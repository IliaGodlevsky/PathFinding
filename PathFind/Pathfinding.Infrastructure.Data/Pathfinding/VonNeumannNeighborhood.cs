using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Shared.Primitives;
using System;
using System.Diagnostics;

namespace Pathfinding.Infrastructure.Data.Pathfinding
{
    [DebuggerDisplay("Count = {Neighbours.Count}")]
    public sealed class VonNeumannNeighborhood : Neighborhood
    {
        public VonNeumannNeighborhood(Coordinate coordinate) : base(coordinate)
        {

        }

        protected override Coordinate[] Filter(Coordinate coordinate)
        {
            return selfCoordinate.IsCardinal(coordinate)
                ? new[] { coordinate }
                : Array.Empty<Coordinate>();
        }
    }
}