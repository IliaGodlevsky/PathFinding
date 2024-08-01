using Pathfinding.Domain.Interface;
using Shared.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Pathfinding.Infrastructure.Data.Pathfinding
{
    public class Coordinate : ICoordinate
    {
        private readonly string toString;
        private readonly int hashCode;

        private IReadOnlyList<int> CoordinatesValues { get; }

        public int Count => CoordinatesValues.Count;

        public int this[int index] => CoordinatesValues[index];

        public Coordinate(int numberOfDimensions, IReadOnlyList<int> coordinates)
        {
            CoordinatesValues = coordinates
                .TakeOrDefault(numberOfDimensions)
                .ToArray();
            toString = $"({string.Join(",", CoordinatesValues)})";
            hashCode = CoordinatesValues.AggregateOrDefault(HashCode.Combine);
        }

        public Coordinate(IReadOnlyList<int> coordinates)
            : this(coordinates.Count, coordinates)
        {

        }

        public Coordinate(params int[] coordinates)
            : this((IReadOnlyList<int>)coordinates)
        {

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => toString;

        public IEnumerator<int> GetEnumerator() => CoordinatesValues.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => CoordinatesValues.GetEnumerator();
    }
}