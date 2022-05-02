using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;
using System.Linq;

namespace GraphLib.Realizations.Extensions
{
    public static class GraphExtensions
    {
        public static IGraph Smooth(this IGraph self, IVertexCostFactory costFactory,
            IMeanCost meanCost, int smoothLevel)
        {
            while (smoothLevel-- > 0)
            {
                var visited = new VisitedVertices();
                foreach (var vertex in self.Vertices)
                {
                    visited.Visit(vertex);
                    if (visited.HasUnvisitedNeighbours(vertex))
                    {
                        int Calculate(IVertex neighbour) => meanCost.Calculate(neighbour, vertex);
                        double avgCost = visited.GetUnvisitedNeighbours(vertex).Average(Calculate);
                        int cost = Convert.ToInt32(Math.Round(avgCost, 0));
                        vertex.Cost = costFactory.CreateCost(cost);
                    }
                }
            }
            return self;
        }
    }
}
