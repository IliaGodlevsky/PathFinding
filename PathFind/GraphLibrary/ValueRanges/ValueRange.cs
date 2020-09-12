namespace GraphLibrary.ValueRanges
{
    public class ValueRange
    {
        public int UpperRange { get; }
        public int LowerRange { get; }

        public ValueRange(int upper, int lower)
        {
            if (upper < lower)
            {
                int temp = upper;
                upper = lower;
                lower = temp;
            }

            UpperRange = upper;
            LowerRange = lower;
        }

        public int ReturnInBounds(int value)
        {
            if (value > UpperRange)
                value = LowerRange;
            else if (value < LowerRange)
                value = UpperRange;
            return value;
        }

        public bool IsInBounds(int value)
        {
            return value <= UpperRange &&
                value >= LowerRange;
        }
    }
}
