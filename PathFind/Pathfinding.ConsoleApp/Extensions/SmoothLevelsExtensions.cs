using Pathfinding.Domain.Core;

namespace Pathfinding.ConsoleApp.Extensions
{
    internal static class SmoothLevelsExtensions
    {
        public static string ToStringRepresentation(this SmoothLevels level)
        {
            return level switch
            {
                SmoothLevels.No => "No",
                SmoothLevels.Low => "Low",
                SmoothLevels.Medium => "Medium",
                SmoothLevels.High => "High",
                SmoothLevels.Extreme => "Extreme",
                _ => "",
            };
        }
    }
}
