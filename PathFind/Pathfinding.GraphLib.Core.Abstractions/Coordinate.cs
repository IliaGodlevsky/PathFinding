using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System.Collections;
using System.Collections.Generic;

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
            CoordinatesValues = coordinates.TakeOrDefault(numberOfDimensions).ToReadOnly();
            toString = $"({string.Join(",", CoordinatesValues)})";
            hashCode = CoordinatesValues.ToHashCode();
        }

        public bool Equals(ICoordinate other)
        {
            return other.GetHashCode().Equals(GetHashCode());
        }

        public override bool Equals(object pos)
        {
            return pos is ICoordinate coord ? Equals(coord) : false;
        }

        public override int GetHashCode() => hashCode;

        public override string ToString() => toString;

        public IEnumerator<int> GetEnumerator() => CoordinatesValues.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => CoordinatesValues.GetEnumerator();
    }
}