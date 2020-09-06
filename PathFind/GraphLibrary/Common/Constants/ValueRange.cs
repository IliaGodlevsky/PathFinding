namespace GraphLibrary.Common.Constants
{
    public class ValueRange
    {
        public ValueRange(int upper, int lower)
        {
            UpperRange = upper;
            LowerRange = lower;
        }

        public bool IsInBounds(int value)
        {
            return value <= UpperRange &&
                value >= LowerRange;
        }

        public int UpperRange { get; }
        public int LowerRange { get; }
    }
}
