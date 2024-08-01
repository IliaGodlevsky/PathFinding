using Pathfinding.Domain.Interface;
using Shared.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Data.Extensions
{
    public static class GraphExtensions
    {
        public static int GetWidth<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IVertex
        {
            return graph.DimensionsSizes.ElementAtOrDefault(0);
        }

        public static int GetLength<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IVertex
        {
            return graph.DimensionsSizes.ElementAtOrDefault(1);
        }

        public static int GetHeight<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IVertex
        {
            return graph.DimensionsSizes.ElementAtOrDefault(2);
        }

        public static int GetObstaclePercent<TVertex>(this IGraph<TVertex> self)
            where TVertex : IVertex
        {
            return (int)Math.Round(self.Count == 0 ? 0 : self.GetObstaclesCount() * 100.0 / self.Count);
        }

        public static void ApplyCosts<T>(this IEnumerable<T> graph, IEnumerable<int> costs)
            where T : IVertex
        {
            foreach (var item in graph.Zip(costs, (v, p) => (Vertex: v, Price: p)))
            {
                var range = item.Vertex.Cost.CostRange;
                int cost = range.ReturnInRange(item.Price);
                item.Vertex.Cost.CurrentCost = cost;
            }
        }

        public static IEnumerable<TVertex> GetObstacles<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IVertex
        {
            return graph.Where(vertex => vertex.IsObstacle);
        }

        public static IEnumerable<ICoordinate> GetCoordinates<TVertex>(this IEnumerable<TVertex> graph)
            where TVertex : IVertex
        {
            return graph.Select(vertex => vertex.Position);
        }

        public static IEnumerable<ICoordinate> GetObstaclesCoordinates<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IVertex
        {
            return graph.GetObstacles().Select(vertex => vertex.Position);
        }

        public static IEnumerable<ICoordinate> GetNotObstaclesCoordinates<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IVertex
        {
            return graph.Where(vertex => !vertex.IsObstacle).Select(vertex => vertex.Position);
        }

        public static int GetObstaclesCount<TVertex>(this IGraph<TVertex> self)
            where TVertex : IVertex
        {
            return self.GetObstacles().Count();
        }

        public static int GetNumberOfNotIsolatedVertices<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IVertex
        {
            return graph.Where(vertex => !vertex.IsIsolated()).Count();
        }
    }
}
