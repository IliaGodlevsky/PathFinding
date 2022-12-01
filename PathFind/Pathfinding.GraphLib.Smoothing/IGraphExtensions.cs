using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Comparers;
using Pathfinding.GraphLib.Smoothing.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Smoothing
{
    public static class IGraphExtensions
    {
        public static void Smooth<TVertex>(this IGraph<TVertex> self, IMeanCost meanCost, int smoothLevel)
            where TVertex : IVertex
        {
            var visited = new HashSet<IVertex>(new VertexEqualityComparer());
            while (smoothLevel-- > 0)
            {
                foreach (var vertex in self)
                {
                    visited.Add(vertex);
                    if (vertex.Neighbours.Any(v => !visited.Contains(v)))
                    {
                        double avgCost = vertex.Neighbours
                            .Where(v => !visited.Contains(v))
                            .Average(neighbour => meanCost.Calculate(neighbour, vertex));
                        vertex.Cost = vertex.Cost.SetCost((int)Math.Round(avgCost, 0));
                    }
                }
                visited.Clear();
            }
        }
    }
}
