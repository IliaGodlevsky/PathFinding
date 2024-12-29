using Pathfinding.ConsoleApp.Resources;
using Pathfinding.Domain.Core;

namespace Pathfinding.ConsoleApp.Extensions
{
    internal static class SmoothLevelsExtensions
    {
        public static string ToStringRepresentation(this SmoothLevels level)
        {
            return level switch
            {
                SmoothLevels.No => Resource.No,
                SmoothLevels.Low => Resource.Low,
                SmoothLevels.Medium => Resource.Medium,
                SmoothLevels.High => Resource.High,
                SmoothLevels.Extreme => Resource.Extreme,
                _ => string.Empty,
            };
        }
    }
}
