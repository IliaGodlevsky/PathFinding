using Common;

namespace WindowsFormsVersion
{
    public static class Constants
    {
        public static ValueRange GraphWidthValueRange { get; }
        public static ValueRange GraphLengthValueRange { get; }
        public static ValueRange ObstaclesPercentValueRange { get; }
        public static ValueRange AlgorithmDelayTimeValueRange { get; }

        static Constants()
        {
            GraphWidthValueRange = new ValueRange(67, 1);
            GraphLengthValueRange = new ValueRange(32, 1);
            ObstaclesPercentValueRange = new ValueRange(99, 0);
            AlgorithmDelayTimeValueRange = new ValueRange(35, 0);
        }
    }
}
