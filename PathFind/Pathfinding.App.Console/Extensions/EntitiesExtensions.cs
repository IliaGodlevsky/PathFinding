using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.Localization;
using Shared.Extensions;
using System;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class EntitiesExtensions
    {
        public static string ConvertToString(this GraphInformationReadDto dto)
        {
            int count = dto.Dimensions.AggregateOrDefault((x, y) => x * y);
            double obstacleCount = dto.ObstaclesCount;
            double obstaclePercent = Math.Round(obstacleCount * 100 / count);
            int width = dto.Dimensions.ElementAtOrDefault(0);
            int length = dto.Dimensions.ElementAtOrDefault(1);
            return string.Format(Languages.GraphParametes, 
                dto.Name, 
                width, 
                length,
                obstaclePercent, 
                dto.ObstaclesCount, 
                count);
        }
    }
}
