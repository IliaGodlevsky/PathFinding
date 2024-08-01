﻿using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using System;

namespace Pathfinding.Infrastructure.Business.MeanCosts
{
    public sealed class MeanCost : IMeanCost
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
