using Common;

namespace WPFVersion3D
{
    public static class Constants
    {
        public static ValueRange ObstaclePercentValueRange { get; }
        public static ValueRange AlgorithmDelayValueRange { get; }
        public static ValueRange GraphWidthValueRange { get; }
        public static ValueRange GraphLengthValueRange { get; }
        public static ValueRange GraphHeightValueRange { get; }

        static Constants()
        {
            ObstaclePercentValueRange = new ValueRange(99, 0);
            AlgorithmDelayValueRange = new ValueRange(35, 0);
            GraphWidthValueRange = new ValueRange(30, 0);
            GraphLengthValueRange = new ValueRange(30, 0);
            GraphHeightValueRange = new ValueRange(30, 0);
        }
    }
}
