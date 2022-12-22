using Pathfinding.GraphLib.Core.Abstractions;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
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

        private readonly int[] OffsetMatrix = new[] { -1, 0, 1 };

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

        private HashSet<ICoordinate> CollectNeighbors(int depth = 0)
        {
            var neighborhood = new HashSet<ICoordinate>();
            foreach (int offset in lateralOffsetMatrix)
            {
                resultCoordinatesValues[depth] = selfCoordinate[depth] + offset;
                var neighbours = depth < limitDepth - 1
                    ? CollectNeighbors(depth + 1).AsEnumerable()
                    : new[] { new NeighborhoodCoordinate(resultCoordinatesValues) };
                neighborhood.AddRange(neighbours);
            }
            return neighborhood;
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