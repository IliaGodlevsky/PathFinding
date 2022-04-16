namespace Common
{
    public sealed class ValueTypeWrap<T> where T : struct
    {
        public T Value { get; set; }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is ValueTypeWrap<T> wrap && wrap.Value.Equals(Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}