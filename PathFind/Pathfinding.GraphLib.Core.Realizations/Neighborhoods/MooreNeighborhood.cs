using Pathfinding.GraphLib.Core.Abstractions;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Neighborhoods
{
    [DebuggerDisplay("Count = {Neighbours.Count}")]
    public sealed class MooreNeighborhood : INeighborhood
    {
        private sealed class NeighborhoodCoordinate : Coordinate
        {
            public NeighborhoodCoordinate(IReadOnlyList<int> coordinates)
                : base(coordinates.Count, coordinates)
            {

            }
        }

        private readonly ICoordinate selfCoordinate;
        private readonly int limitDepth;
        private readonly int[] resultCoordinatesValues;
        private readonly Lazy<IReadOnlyCollection<ICoordinate>> neighbourhood;

        private IReadOnlyCollection<ICoordinate> Neighbours => neighbourhood.Value;

        public int Count => Neighbours.Count;

        public MooreNeighborhood(ICoordinate coordinate)
        {
            selfCoordinate = coordinate;
            limitDepth = selfCoordinate.Count;
            resultCoordinatesValues = new int[limitDepth];
            neighbourhood = new(GetNeighborhood, true);
        }

        private List<ICoordinate> CollectNeighbors(int depth = 0)
        {
            var neighborhood = new List<ICoordinate>();
            for (int offset = -1; offset <= 1; offset++)
            {
                resultCoordinatesValues[depth] = selfCoordinate[depth] + offset;
                var neighbours = IsBottom(depth)
                    ? CollectNeighbors(depth + 1).AsEnumerable()
                    : GetCoordinate();
                neighborhood.AddRange(neighbours);
            }
            return neighborhood;
        }

        private bool IsBottom(int depth)
        {
            return depth < limitDepth - 1;
        }

        private IEnumerable<NeighborhoodCoordinate> GetCoordinate()
        {
            yield return new(resultCoordinatesValues);
        }

        private IReadOnlyCollection<ICoordinate> GetNeighborhood()
        {
            var coordinates = CollectNeighbors();
            coordinates.Remove(selfCoordinate);
            return coordinates;
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