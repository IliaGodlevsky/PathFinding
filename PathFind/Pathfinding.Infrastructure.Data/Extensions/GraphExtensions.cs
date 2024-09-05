using Pathfinding.Domain.Interface;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Data.Extensions
{
    public static class GraphExtensions
    {
        public static TVertex Get<TVertex>(this IGraph<TVertex> graph, int x, int y)
            where TVertex : IVertex
        {
            return graph.Get(new Coordinate(x, y));
        }

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

        public static void ApplyCosts<T>(this IEnumerable<T> graph, IEnumerable<int> costs)
            where T : IVertex
        {
            foreach (var (Vertex, Price) in graph.Zip(costs, (v, p) => (Vertex: v, Price: p)))
            {
                var range = Vertex.Cost.CostRange;
                int cost = range.ReturnInRange(Price);
                Vertex.Cost.CurrentCost = cost;
            }
        }

        public static IEnumerable<TVertex> GetObstacles<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IVertex
        {
            return graph.Where(vertex => vertex.IsObstacle);
        }

        public static IEnumerable<Coordinate> GetCoordinates<TVertex>(this IEnumerable<TVertex> graph)
            where TVertex : IVertex
        {
            return graph.Select(vertex => vertex.Position);
        }

        public static int GetObstaclesCount<TVertex>(this IGraph<TVertex> self)
            where TVertex : IVertex
        {
            return self.GetObstacles().Count();
        }
    }
}
