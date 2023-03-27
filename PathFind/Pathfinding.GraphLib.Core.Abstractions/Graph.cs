using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Comparers;
using Shared.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Abstractions
{
    public abstract class Graph<TVertex> : IGraph<TVertex>
        where TVertex : IVertex
    {
        protected readonly IReadOnlyDictionary<ICoordinate, TVertex> vertices;

        public int Count { get; }

        public IReadOnlyList<int> DimensionsSizes { get; }

        protected Graph(int requiredNumberOfDimensions, 
            IReadOnlyCollection<TVertex> vertices,
            IReadOnlyList<int> dimensionSizes)
        {
            DimensionsSizes = dimensionSizes
                .TakeOrDefault(requiredNumberOfDimensions, 1)
                .ToArray();
            Count = DimensionsSizes.AggregateOrDefault((x, y) => x * y);
            var comparer = new CoordinateEqualityComparer();
            this.vertices = vertices.Take(Count)
                .ToDictionary(vertex => vertex.Position, comparer)
                .AsReadOnly();
        }

        public TVertex Get(ICoordinate coordinate)
        {
            if (vertices.TryGetValue(coordinate, out var vertex))
            {
                return vertex;
            }

            throw new KeyNotFoundException();
        }

        public IEnumerator<TVertex> GetEnumerator()
        {
            return vertices.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}