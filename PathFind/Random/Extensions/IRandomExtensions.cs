using Random.Interface;
using ValueRange;

namespace Random.Extensions
{
    public static class IRandomExtensions
    {
        public static int Next(this IRandom self, InclusiveValueRange<int> range)
        {
            return self.Next(range.LowerValueOfRange, range.UpperValueOfRange);
        }

        public static int Next(this IRandom random)
        {
            return random.Next(default, int.MaxValue);
        }
    }
}
