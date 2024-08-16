using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Pathfinding.Infrastructure.Business.Benchmarks.Data
{
    internal sealed class BenchmarkVertex : IVertex
    {
        public bool IsObstacle { get; set; }

        public IVertexCost Cost { get; set; }

        public Coordinate Position { get; }

        public ICollection<IVertex> Neighbours { get; } = new HashSet<IVertex>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BenchmarkVertex(Coordinate coordinate)
        {
            Position = coordinate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(IVertex other)
        {
            return other.IsEqual(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return obj is BenchmarkVertex vert && Equals(vert);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }
}
