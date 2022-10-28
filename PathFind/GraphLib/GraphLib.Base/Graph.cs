using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Reflection.BindingFlags;

namespace GraphLib.Base
{
    public abstract class Graph : IGraph
    {
        private readonly Type graphType;
        private readonly object locker = new object();
        private readonly IReadOnlyDictionary<ICoordinate, IVertex> vertices;

        public int Count { get; }

        public IReadOnlyList<int> DimensionsSizes { get; }

        protected Graph(int requiredNumberOfDimensions, IReadOnlyCollection<IVertex> vertices,
            IReadOnlyList<int> dimensionSizes)
        {
            graphType = GetType();
            DimensionsSizes = dimensionSizes.TakeOrDefault(requiredNumberOfDimensions, 1).ToReadOnly();
            Count = DimensionsSizes.AggregateOrDefault((x, y) => x * y);
            this.vertices = vertices.Take(Count)
                .ForEach(SetGraph)
                .ToDictionary(vertex => vertex.Position, new CoordinateEqualityComparer())
                .ToReadOnly();
        }

        public IVertex Get(ICoordinate coordinate)
        {
            return vertices.GetOrNullVertex(coordinate);
        }

        public override bool Equals(object obj)
        {
            if (obj is IGraph graph)
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

        private void SetGraph(IVertex vertex)
        {
            lock (locker)
            {
                vertex
                  .GetType()
                  .GetFields(NonPublic | Instance)
                  .Where(field => field.FieldType.IsAssignableFrom(graphType))
                  .ForEach(info => info.SetValue(vertex, this));
            }
        }

        public IEnumerator<IVertex> GetEnumerator() => vertices.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}