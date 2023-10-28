using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Comparers;
using Shared.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations
{
    public class Graph<TVertex> : IGraph<TVertex>
        where TVertex : IVertex
    {
        public static readonly Graph<TVertex> Empty = new() { Id = Guid.Empty };

        private readonly IReadOnlyDictionary<ICoordinate, TVertex> vertices;

        public Guid Id { get; private set; } = Guid.NewGuid();

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