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
            var visitedVertices = new VisitedVertices();
            return graphAssemble
                .AssembleGraph(obstaclePercent, graphDimensionSizes)
                .ForEach(vertex => SmoothOut(vertex, visitedVertices));
        }

        private void SmoothOut(IVertex vertex, IVisitedVertices visitedVertices)
        {
            visitedVertices.Add(vertex);
            var unvisitedNeighbours = visitedVertices.GetUnvisitedNeighbours(vertex);
            int AverageCost(IVertex neighbour) => averageCost.Calculate(neighbour, vertex);
            if (unvisitedNeighbours.Any())
            {
                double avgCost = unvisitedNeighbours.Average(AverageCost);
                int cost = Convert.ToInt32(Math.Round(avgCost, 0));
                vertex.Cost = costFactory.CreateCost(cost);
            }
        }

        private readonly IGraphAssemble graphAssemble;
        private readonly IVertexCostFactory costFactory;
        private readonly IMeanCost averageCost;
    }
}