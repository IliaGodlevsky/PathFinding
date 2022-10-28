namespace ValueRange
{
    public static class ValueRanges
    {
        public static readonly InclusiveValueRange<int> IntRange
            = new InclusiveValueRange<int>(int.MaxValue, int.MinValue);
    }
}
