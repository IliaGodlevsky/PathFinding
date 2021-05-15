using AssembleClassesLib.Attributes;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.AverageCosts;
using GraphLib.Realizations.VisitedVerticesImpl;
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
           ICoordinateRadarFactory radarFactory,
           IAverageCost averageCost)
            : base(vertexFactory, coordinateFactory,
                  graphFactory, costFactory, radarFactory)
        {
            this.averageCost = averageCost;
            visitedVertices = new VisitedVertices();
        }

        public SmoothedGraphAssemble(
           IVertexFactory vertexFactory,
           ICoordinateFactory coordinateFactory,
           IGraphFactory graphFactory,
           IVertexCostFactory costFactory,
           ICoordinateRadarFactory radarFactory)
            : this(vertexFactory, coordinateFactory,
                  graphFactory, costFactory, radarFactory, new MeanCost())
        {

        }

        public override IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionSizes)
        {
            visitedVertices.Clear();
            return base
                .AssembleGraph(obstaclePercent, graphDimensionSizes)
                .ForEach(SmoothOut);            
        }

        private void SmoothOut(IVertex vertex)
        {
            visitedVertices.Add(vertex);
            var unvisitedNeighbours = visitedVertices.GetUnvisitedNeighbours(vertex).ToList();
            int AverageCost(IVertex neighbour) => averageCost.Calculate(neighbour, vertex);
            if (unvisitedNeighbours.Any())
            {
                double avgCost = unvisitedNeighbours.Average(AverageCost);
                int cost = Convert.ToInt32(Math.Round(avgCost, 0));
                vertex.Cost = costFactory.CreateCost(cost);
            }
        }

        protected readonly IAverageCost averageCost;
        private readonly IVisitedVertices visitedVertices;
    }
}
