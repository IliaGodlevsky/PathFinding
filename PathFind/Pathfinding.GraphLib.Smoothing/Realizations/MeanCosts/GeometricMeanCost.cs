using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Smoothing.Interface;
using System;

namespace Pathfinding.GraphLib.Smoothing.Realizations.MeanCosts
{
    public sealed class GeometricMeanCost : IMeanCost
    {
        public int Calculate(IVertex neighbour, IVertex vertex)
        {
            int neighbourCost = neighbour.Neighbours[vertex].CurrentCost;
            int vertexCost = vertex.Neighbours[neighbour].CurrentCost;
            double averageCost = Math.Sqrt(vertexCost * neighbourCost);
            double roundAverageCost = Math.Round(averageCost, 0);
            return Convert.ToInt32(roundAverageCost);
        }
    }
}
