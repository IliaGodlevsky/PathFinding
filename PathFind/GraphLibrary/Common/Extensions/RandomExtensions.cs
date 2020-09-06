using GraphLibrary.Common.Constants;
using System;

namespace GraphLibrary.Extensions.RandomExtension
{
    public static class RandomExtensions
    {        
        public static bool IsObstacleChance(this Random rand, int percentOfObstacles)
        {
            return rand.Next(Range.ObstaclePercentValueRange.UpperRange) < percentOfObstacles;
        }
    }
}
