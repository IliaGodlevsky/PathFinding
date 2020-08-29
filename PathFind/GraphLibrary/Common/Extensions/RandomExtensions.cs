using GraphLibrary.Common.Constants;
using System;

namespace GraphLibrary.Extensions.RandomExtension
{
    public static class RandomExtensions
    {        
        public static bool IsObstacleChance(this Random rand, int percentOfObstacles)
        {
            const int MAX_PERCENT_OF_OBSTACLES = 100;
            return rand.Next(MAX_PERCENT_OF_OBSTACLES) < percentOfObstacles;
        }
    }
}
