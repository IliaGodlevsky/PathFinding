using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;
using System.Linq;

namespace GraphLib.Realizations.Extensions
{
    public static class GraphExtensions
    {
        public static void Smooth(this IGraph self, IVertexCostFactory costFactory,
            IMeanCost meanCost, int smoothLevel)
        {
            var visited = new VisitedVertices();
            while (smoothLevel-- > 0)
            {
                self.ForEach(vertex =>
                {
                    visited.Visit(vertex);
                    if (vertex.Neighbours.Any(visited.IsNotVisited))
                    {
                        double avgCost = vertex.Neighbours
                            .Where(visited.IsNotVisited)
                            .Average(neighbour => meanCost.Calculate(neighbour, vertex));
                        vertex.Cost = costFactory.CreateCost((int)Math.Round(avgCost, 0));
                    }
                });
                visited.Clear();
            }
        }
    }
}
