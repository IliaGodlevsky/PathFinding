using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.Extensions
{
    public static class GraphExtensions
    {
        public static void Smooth<TVertex>(this IGraph<TVertex> self, IVertexCostFactory costFactory,
            IMeanCost meanCost, int smoothLevel)
            where TVertex : IVertex
        {
            var visited = new HashSet<IVertex>(new VertexEqualityComparer());
            while (smoothLevel-- > 0)
            {
                self.ForEach(vertex =>
                {
                    visited.Add(vertex);
                    if (vertex.Neighbours.Any(v => !visited.Contains(v)))
                    {
                        double avgCost = vertex.Neighbours
                            .Where(v => !visited.Contains(v))
                            .Average(neighbour => meanCost.Calculate(neighbour, vertex));
                        vertex.Cost = costFactory.CreateCost((int)Math.Round(avgCost, 0));
                    }
                });
                visited.Clear();
            }
        }
    }
}
