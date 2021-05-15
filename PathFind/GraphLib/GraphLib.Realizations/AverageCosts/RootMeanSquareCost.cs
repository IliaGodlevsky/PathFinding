using GraphLib.Interfaces;
using System;

namespace GraphLib.Realizations.AverageCosts
{
    public sealed class RootMeanSquareCost : IAverageCost
    {
        public int Calculate(IVertex neighbour, IVertex vertex)
        {
            int neighbourCost = neighbour.Cost.CurrentCost;
            int vertexCost = vertex.Cost.CurrentCost;
            double squareMean = (Math.Pow(vertexCost, 2) + Math.Pow(neighbourCost, 2)) / 2;
            double averageCost = Math.Sqrt(squareMean);
            double roundAverageCost = Math.Round(averageCost, 0);
            return Convert.ToInt32(roundAverageCost);
        }
    }
}
