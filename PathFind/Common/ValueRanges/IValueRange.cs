namespace Common.ValueRanges
{
    public interface IValueRange
    {
        int UpperValueOfRange { get; }
        int LowerValueOfRange { get; }

        int ReturnInRange(int value);

        bool Contains(int value);

        int GetRandomValueFromRange();
    }
}
