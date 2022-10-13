using GraphLib.Interfaces;
using GraphLib.Proxy.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GraphLib.Realizations.Neighbourhoods
{
    [DebuggerDisplay("Count = {Neighbours.Count}")]
    public sealed class MooreNeighborhood : INeighborhood
    {
        private static readonly int[] OffsetMatrix = new[] { -1, 0, 1 };

        private readonly ICoordinate selfCoordinate;
        private readonly int limitDepth;
        private readonly int[] resultCoordinatesValues;
        private readonly int[] lateralOffsetMatrix;
        private readonly Lazy<IReadOnlyCollection<ICoordinate>> neighbourhood;

        private IReadOnlyCollection<ICoordinate> Neighbours => neighbourhood.Value;

        public int Count => Neighbours.Count;

        public MooreNeighborhood(ICoordinate coordinate)
        {
            selfCoordinate = coordinate;
            limitDepth = selfCoordinate.Count;
            resultCoordinatesValues = new int[limitDepth];
            lateralOffsetMatrix = limitDepth == 0 ? Array.Empty<int>() : OffsetMatrix;
            neighbourhood = new Lazy<IReadOnlyCollection<ICoordinate>>(GetNeighborhood);
        }

        private List<ICoordinate> CollectNeighbors(int depth = 0)
        {
            var neighborhood = new List<ICoordinate>();
            foreach (int offset in lateralOffsetMatrix)
            {
                resultCoordinatesValues[depth] = selfCoordinate[depth] + offset;
                var neighbours = depth < limitDepth - 1
                    ? CollectNeighbors(depth + 1).AsEnumerable()
                    : new[] { resultCoordinatesValues.ToCoordinate() };
                neighborhood.AddRange(neighbours);
            }
            return neighborhood;
        }

        private IReadOnlyCollection<ICoordinate> GetNeighborhood()
        {
            var coordinates = CollectNeighbors();
            coordinates.Remove(selfCoordinate);
            return coordinates.AsReadOnly();
        }

        public IEnumerator<ICoordinate> GetEnumerator()
        {
            return Neighbours.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}