using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.MeanCosts;
using System;
using System.ComponentModel;
using System.Linq;

namespace GraphLib.Realizations.Factories.GraphAssembles
{
    [Description("Smoothed graph assemble")]
    public class SmoothedGraphAssemble : IGraphAssemble
    {
        public SmoothedGraphAssemble(
           IGraphAssemble graphAssemble,
           IVertexCostFactory costFactory,
           IMeanCost averageCost)
        {
            this.graphAssemble = graphAssemble;
            this.costFactory = costFactory;
            this.averageCost = averageCost;
        }

        public SmoothedGraphAssemble(
           IGraphAssemble graphAssemble,
           IVertexCostFactory costFactory)
            : this(graphAssemble, costFactory, new MeanCost())
        {

        }

        public IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionSizes)
        {
            var visited = new VisitedVertices();
            var graph = graphAssemble.AssembleGraph(obstaclePercent, graphDimensionSizes);
            foreach (var vertex in graph.Vertices)
            {
                visited.Add(vertex);
                if (visited.HasUnvisitedNeighbours(vertex))
                {
                    double avgCost = visited
                        .GetUnvisitedNeighbours(vertex)
                        .Average(neighbour => averageCost.Calculate(neighbour, vertex));
                    int cost = Convert.ToInt32(Math.Round(avgCost, 0));
                    vertex.Cost = costFactory.CreateCost(cost);
                }
            }
            return graph;
        }

        private readonly IGraphAssemble graphAssemble;
        private readonly IVertexCostFactory costFactory;
        private readonly IMeanCost averageCost;
    }
}