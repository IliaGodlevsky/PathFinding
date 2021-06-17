using AssembleClassesLib.Attributes;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.MeanCosts;
using System;
using System.Linq;

namespace GraphLib.Realizations.Factories.GraphAssembles
{
    [ClassName("Smoothed graph assemble")]
    public class SmoothedGraphAssemble : GraphAssemble
    {
        public SmoothedGraphAssemble(
           IVertexFactory vertexFactory,
           ICoordinateFactory coordinateFactory,
           IGraphFactory graphFactory,
           IVertexCostFactory costFactory,
           INeighboursCoordinatesFactory radarFactory,
           IMeanCost averageCost)
            : base(vertexFactory, coordinateFactory,
                  graphFactory, costFactory, radarFactory)
        {
            this.averageCost = averageCost;
        }

        public SmoothedGraphAssemble(
           IVertexFactory vertexFactory,
           ICoordinateFactory coordinateFactory,
           IGraphFactory graphFactory,
           IVertexCostFactory costFactory,
           INeighboursCoordinatesFactory radarFactory)
            : this(vertexFactory, coordinateFactory,
                  graphFactory, costFactory, radarFactory, new MeanCost())
        {

        }

        public override IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionSizes)
        {
            var visitedVertices = new VisitedVertices();
            return base
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

        protected readonly IMeanCost averageCost;
    }
}
