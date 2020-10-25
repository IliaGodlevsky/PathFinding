using GraphLibrary.ValueRanges;
using GraphLibrary.Vertex.Cost;
using System;

namespace GraphLibrary.Extensions.SystemTypeExtensions
{
    public static class RandomExtensions
    {
        public static bool IsObstacleChance(this Random rand, int percentOfObstacles)
        {
            return rand.Next(Range.ObstaclePercentValueRange.UpperRange) < percentOfObstacles;
        }

        public static VertexCost GetRandomValueCost(this Random rand)
        {
            var cost = rand.Next(Range.VertexCostRange.UpperRange) 
                + Range.VertexCostRange.LowerRange;
            return new VertexCost(cost);
        }
    }
}
