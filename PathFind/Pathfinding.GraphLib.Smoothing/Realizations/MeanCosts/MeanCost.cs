using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Smoothing.Interface;
using System;

namespace Pathfinding.GraphLib.Smoothing.Realizations.MeanCosts
{
    public sealed class MeanCost : IMeanCost
    {
        public int Calculate(IVertex neighbour, IVertex vertex)
        {
            int neighbourCost = neighbour.Neighbours[vertex].CurrentCost;
            int vertexCost = vertex.Neighbours[neighbour].CurrentCost;
            double averageCost = (vertexCost + neighbourCost) / 2;
            double roundAverageCost = Math.Ceiling(averageCost);
            return Convert.ToInt32(roundAverageCost);
        }
    }
}
