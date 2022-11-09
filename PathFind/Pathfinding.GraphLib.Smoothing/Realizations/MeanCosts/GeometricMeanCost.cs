﻿using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Smoothing.Interface;
using System;

namespace Pathfinding.GraphLib.Smoothing.Realizations.MeanCosts
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
