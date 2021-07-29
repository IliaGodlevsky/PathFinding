using Common.Extensions;
using Common.ValueRanges;

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
