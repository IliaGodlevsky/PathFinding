using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pathfinding.Infrastructure.Data.Pathfinding
{
    [DebuggerDisplay("Count = {Neighbours.Count}")]
    public sealed class MooreNeighborhood : INeighborhood
    {
        private readonly Coordinate selfCoordinate;
        private readonly int limitDepth;
        private readonly int[] resultCoordinatesValues;
        private readonly Lazy<IReadOnlyCollection<Coordinate>> neighbourhood;

        public int Count => neighbourhood.Value.Count;

        public MooreNeighborhood(Coordinate coordinate)
        {
            selfCoordinate = coordinate;
            limitDepth = selfCoordinate.Count;
            resultCoordinatesValues = new int[limitDepth];
            neighbourhood = new Lazy<IReadOnlyCollection<Coordinate>>(GetNeighborhood, true);
        }

        private HashSet<Coordinate> CollectNeighbors(int depth = 0)
        {
            var neighborhood = new HashSet<Coordinate>();
            for (int offset = -1; offset <= 1; offset++)
            {
                resultCoordinatesValues[depth] = selfCoordinate[depth] + offset;
                var neighbours = IsBottom(depth)
                    ? CollectNeighbors(depth + 1).AsEnumerable()
                    : new[] { new Coordinate(resultCoordinatesValues) };
                neighborhood.AddRange(neighbours);
            }
            return neighborhood;
        }

        private bool IsBottom(int depth)
        {
            return depth < limitDepth - 1;
        }

        private IReadOnlyCollection<Coordinate> GetNeighborhood()
        {
            var coordinates = CollectNeighbors();
            coordinates.Remove(selfCoordinate);
            return coordinates;
        }

        public IEnumerator<Coordinate> GetEnumerator()
        {
            return neighbourhood.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}