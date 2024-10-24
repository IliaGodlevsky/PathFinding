using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Comparers;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Data.Pathfinding
{
    public class Graph<TVertex> : IGraph<TVertex>
        where TVertex : IVertex
    {
        public static readonly Graph<TVertex> Empty = new Graph<TVertex>();

        private readonly IReadOnlyDictionary<Coordinate, TVertex> vertices;

        public IReadOnlyList<int> DimensionsSizes { get; }

        public int Count { get; }

        public Graph(int requiredNumberOfDimensions,
             IReadOnlyCollection<TVertex> vertices,
             IReadOnlyList<int> dimensionSizes)
        {
            DimensionsSizes = dimensionSizes
                .TakeOrDefault(requiredNumberOfDimensions, 1)
                .ToArray();
            Count = DimensionsSizes.AggregateOrDefault((x, y) => x * y);
            this.vertices = vertices.Take(Count)
                .ToDictionary(vertex => vertex.Position, CoordinateEqualityComparer.Interface)
                .AsReadOnly();
        }

        public Graph(IReadOnlyCollection<TVertex> vertices,
             IReadOnlyList<int> dimensionSizes)
            : this(dimensionSizes.Count, vertices, dimensionSizes)
        {

        }

        public Graph(IReadOnlyCollection<TVertex> vertices,
             params int[] dimensionSizes)
            : this(vertices, (IReadOnlyList<int>)dimensionSizes)
        {

        }

        protected Graph()
            : this(Array.Empty<TVertex>())
        {

        }

        public TVertex Get(Coordinate coordinate)
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