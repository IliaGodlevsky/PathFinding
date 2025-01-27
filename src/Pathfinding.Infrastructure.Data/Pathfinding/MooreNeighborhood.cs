using Pathfinding.Shared.Primitives;
using System.Diagnostics;

namespace Pathfinding.Infrastructure.Data.Pathfinding
{
    [DebuggerDisplay("Count = {Neighbours.Count}")]
    public sealed class MooreNeighborhood : Neighborhood
    {
        public MooreNeighborhood(Coordinate coordinate) : base(coordinate)
        {

        }

        protected override Coordinate[] Filter(Coordinate coordinate)
        {
            return new[] { coordinate };
        }
    }
}