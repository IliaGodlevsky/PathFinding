using Common.ValueRanges;
using GraphLib.Vertex.Cost;
using System;

namespace GraphLib.Extensions
{
    public static class RandomExtensions
    {
        internal static bool IsObstacleChance(this Random rand, int percentOfObstacles)
        {
            var randomValue = rand.Next(Range.ObstaclePercentValueRange.UpperRange);
            return randomValue < percentOfObstacles;
        }

        internal static VertexCost GetRandomValueCost(this Random rand)
        {
            var cost = rand.Next(Range.VertexCostRange.UpperRange)
                + Range.VertexCostRange.LowerRange;

            return new VertexCost(cost);
        }
    }
}
