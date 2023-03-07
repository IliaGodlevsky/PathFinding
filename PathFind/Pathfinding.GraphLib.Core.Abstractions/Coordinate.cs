using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Pathfinding.GraphLib.Core.Abstractions
{
    public abstract class Coordinate : ICoordinate
    {
        private readonly string toString;
        private readonly int hashCode;

        private IReadOnlyList<int> CoordinatesValues { get; }

        public int Count => CoordinatesValues.Count;

        public int this[int index] => CoordinatesValues[index];

        protected Coordinate(int numberOfDimensions, IReadOnlyList<int> coordinates)
        {
            CoordinatesValues = coordinates
                .TakeOrDefault(numberOfDimensions)
                .ToArray();
            toString = $"({string.Join(",", CoordinatesValues)})";
            hashCode = CoordinatesValues.ToHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ICoordinate other)
        {
            return other.GetHashCode().Equals(GetHashCode());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object pos)
        {
            return pos is ICoordinate coord ? Equals(coord) : false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => hashCode;

        public override string ToString() => toString;

        public IEnumerator<int> GetEnumerator() => CoordinatesValues.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => CoordinatesValues.GetEnumerator();
    }
}