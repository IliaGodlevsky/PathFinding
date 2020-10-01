namespace GraphLibrary.ValueRanges
{
    public static class Range
    {
        public static ValueRange ObstaclePercentValueRange { get; }
        public static ValueRange DelayValueRange { get; }
        public static ValueRange WidthValueRange { get; }
        public static ValueRange HeightValueRange { get; }
        public static ValueRange VertexCostRange { get; }
        public static ValueRange VertexSizeRange { get; }

        static Range()
        {
            ObstaclePercentValueRange = new ValueRange(99, 0);
            DelayValueRange = new ValueRange(35, 1);
            WidthValueRange = new ValueRange(100, 0);
            HeightValueRange = new ValueRange(100, 0);
            VertexCostRange = new ValueRange(9, 1);
            VertexSizeRange = new ValueRange(30, 10);
        }
    }
}
