using Common.ValueRanges;
using GraphLib.Vertex.Cost;
using System;

namespace GraphLib.Extensions
{
    public static class RandomExtensions
    {
        internal static bool IsObstacleChance(this Random rand, int percentOfObstacles)
        {
            var percentRange = new ValueRange(100, 0);
            percentOfObstacles = percentRange.ReturnInRange(percentOfObstacles);
            var randomValue = rand.Next(Range.ObstaclePercentValueRange.UpperValueOfRange);
            return randomValue < percentOfObstacles;
        }

        /// <summary>
        /// Creates random value of vertex
        /// </summary>
        /// <param name="rand"></param>
        /// <returns></returns>
        internal static VertexCost GetRandomValueCost(this Random rand)
        {
            var cost = rand.Next(Range.VertexCostRange.UpperValueOfRange)
                + Range.VertexCostRange.LowerValueOfRange;

            return new VertexCost(cost);
        }
    }
}
