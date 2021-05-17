using GraphLib.Interfaces;
using System;

namespace GraphLib.Realizations.AverageCosts
{
    public sealed class MeanCost : IAverageCost
    {
        public int Calculate(IVertex neighbour, IVertex vertex)
        {
            int neighbourCost = neighbour.Cost.CurrentCost;
            int vertexCost = vertex.Cost.CurrentCost;
            double averageCost = (vertexCost + neighbourCost) / 2;
            double roundAverageCost = Math.Ceiling(averageCost);
            return Convert.ToInt32(roundAverageCost);
        }
    }
}
