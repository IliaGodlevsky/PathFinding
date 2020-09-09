using GraphLibrary.ValueRanges;
using System;

namespace GraphLibrary.Extensions.SystemTypeExtensions
{
    public static class RandomExtensions
    {
        public static bool IsObstacleChance(this Random rand, int percentOfObstacles)
        {
            return rand.Next(Range.ObstaclePercentValueRange.UpperRange) < percentOfObstacles;
        }

        public static int GetRandomValueCost(this Random rand)
        {
            return rand.Next(Range.VertexCostRange.UpperRange) + Range.VertexCostRange.LowerRange;
        }
    }
}
