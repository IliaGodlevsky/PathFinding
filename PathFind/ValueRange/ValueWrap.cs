namespace ValueRange
{
    internal sealed class ValueWrap<T>
    {
        public T Value { get; set; }

        public ValueWrap(T value)
        {
            Value = value;
        }
    }
}
