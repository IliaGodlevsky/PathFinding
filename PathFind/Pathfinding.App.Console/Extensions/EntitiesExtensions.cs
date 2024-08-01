using Pathfinding.App.Console.Localization;
using Pathfinding.Service.Interface.Models.Read;
using Shared.Extensions;
using System;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class EntitiesExtensions
    {
        public static string ConvertToString(this GraphInformationModel model)
        {
            int count = model.Dimensions.AggregateOrDefault((x, y) => x * y);
            double obstacleCount = model.ObstaclesCount;
            double obstaclePercent = Math.Round(obstacleCount * 100 / count);
            int width = model.Dimensions.ElementAtOrDefault(0);
            int length = model.Dimensions.ElementAtOrDefault(1);
            return string.Format(Languages.GraphParametes,
                model.Name,
                width,
                length,
                obstaclePercent,
                model.ObstaclesCount,
                count);
        }
    }
}
