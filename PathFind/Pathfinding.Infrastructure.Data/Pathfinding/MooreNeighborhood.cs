using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Shared.Extensions;
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
        private readonly ICoordinate selfCoordinate;
        private readonly int limitDepth;
        private readonly int[] resultCoordinatesValues;
        private readonly Lazy<IReadOnlyCollection<ICoordinate>> neighbourhood;

        public int Count => neighbourhood.Value.Count;

        public MooreNeighborhood(ICoordinate coordinate)
        {
            selfCoordinate = coordinate;
            limitDepth = selfCoordinate.Count;
            resultCoordinatesValues = new int[limitDepth];
            neighbourhood = new Lazy<IReadOnlyCollection<ICoordinate>>(GetNeighborhood, true);
        }

        private HashSet<ICoordinate> CollectNeighbors(int depth = 0)
        {
            var neighborhood = new HashSet<ICoordinate>();
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

        private IReadOnlyCollection<ICoordinate> GetNeighborhood()
        {
            var coordinates = CollectNeighbors();
            coordinates.Remove(selfCoordinate);
            return coordinates;
        }

        public IEnumerator<ICoordinate> GetEnumerator()
        {
            return neighbourhood.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}