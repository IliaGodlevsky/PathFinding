using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.Localization;
using System;

namespace Pathfinding.App.Console.Extensions
{
    internal static class EntitiesExtensions
    {
        public static string ConvertToString(this GraphEntity entity)
        {
            int count = entity.Width * entity.Length;
            double obstacleCount = entity.ObstaclesCount;
            double obstaclePercent = Math.Round(obstacleCount * 100 / count);
            return string.Format(Languages.GraphParametes, entity.Width, entity.Length,
                obstaclePercent, entity.ObstaclesCount, count);
        }
    }
}
