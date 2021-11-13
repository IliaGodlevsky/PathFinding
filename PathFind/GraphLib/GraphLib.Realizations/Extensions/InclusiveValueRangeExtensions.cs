using Common.Extensions;
using Random.Extensions;
using ValueRange;

namespace GraphLib.Realizations.Extensions
{
    internal static class InclusiveValueRangeExtensions
    {
        public static bool IsObstacleChance(this InclusiveValueRange<int> valueRange, int obstaclePercent)
        {
            return valueRange.GetRandomValue().IsLess(obstaclePercent);
        }
    }
}
