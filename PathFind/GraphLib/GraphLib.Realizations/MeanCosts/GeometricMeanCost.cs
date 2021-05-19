using GraphLib.Interfaces;
using System;

namespace GraphLib.Realizations.MeanCosts
{
    public sealed class GeometricMeanCost : IMeanCost
    {
        public int Calculate(IVertex neighbour, IVertex vertex)
        {
            int neighbourCost = neighbour.Cost.CurrentCost;
            int vertexCost = vertex.Cost.CurrentCost;
            double averageCost = Math.Sqrt(vertexCost * neighbourCost);
            double roundAverageCost = Math.Round(averageCost, 0);
            return Convert.ToInt32(roundAverageCost);
        }
    }
}
