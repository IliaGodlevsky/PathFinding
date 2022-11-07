using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Abstractions
{
    public abstract class Graph<TVertex> : IGraph<TVertex>
        where TVertex : IVertex
    {
        private readonly IReadOnlyDictionary<ICoordinate, TVertex> vertices;

        public int Count { get; }

        public IReadOnlyList<int> DimensionsSizes { get; }

        protected Graph(int requiredNumberOfDimensions, IReadOnlyCollection<TVertex> vertices,
            IReadOnlyList<int> dimensionSizes)
        {
            DimensionsSizes = dimensionSizes.TakeOrDefault(requiredNumberOfDimensions, 1).ToReadOnly();
            Count = DimensionsSizes.AggregateOrDefault((x, y) => x * y);
            this.vertices = vertices.Take(Count)
                .ToDictionary(vertex => vertex.Position, new CoordinateEqualityComparer())
                .ToReadOnly();
        }

        public TVertex Get(ICoordinate coordinate)
        {
            if (vertices.TryGetValue(coordinate, out var vertex))
            {
                return vertex;
            }

            throw new KeyNotFoundException();
        }

        public override bool Equals(object obj)
        {
            if (obj is IGraph<TVertex> graph)
            {
                bool hasEqualDimensionSizes = DimensionsSizes.SequenceEqual(graph.DimensionsSizes);
                bool hasEqualNumberOfObstacles = graph.GetObstaclesCount() == this.GetObstaclesCount();
                bool hasEqualVertices = graph.Juxtapose(this, (a, b) => a.Equals(b));
                return hasEqualNumberOfObstacles && hasEqualVertices && hasEqualDimensionSizes;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var verticesHashCode = vertices.Values.Select(x => x.GetHashCode()).ToHashCode();
            var dimensionsHashCode = DimensionsSizes.ToHashCode();
            return HashCode.Combine(verticesHashCode, dimensionsHashCode);
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