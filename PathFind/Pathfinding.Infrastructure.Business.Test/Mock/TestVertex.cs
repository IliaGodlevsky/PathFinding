using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.Infrastructure.Business.Test.Mock
{
    internal sealed class TestVertex : IVertex
    {
        public bool IsObstacle { get; set; }

        public IVertexCost Cost { get; set; } = NullCost.Interface;

        public Coordinate Position { get; }

        public ICollection<IVertex> Neighbours { get; } = new HashSet<IVertex>();

        public TestVertex(Coordinate coordinate)
        {
            Position = coordinate;
        }

        public bool Equals(IVertex? other)
        {
            return other.IsEqual(this);
        }

        public override bool Equals(object? obj)
        {
            return obj is IVertex vertex && Equals(vertex);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }

        public override string? ToString()
        {
            return Position.ToString();
        }
    }
}
