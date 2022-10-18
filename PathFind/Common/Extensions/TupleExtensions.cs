namespace Common.Extensions
{
    public static class TupleExtensions
    {
        public static Enumerator GetEnumerator(this (int LowerValueOfRange, int UpperValueOfRange) range)
        {
            return new Enumerator(range.LowerValueOfRange, range.UpperValueOfRange);
        }
    }

    public ref struct ReverseEnumerator
    {
        private readonly int lowerValueOfRange;
        private readonly int upperValueOfRange;

        public int Current { get; private set; }

        internal ReverseEnumerator(int lowerValueOfRange, int upperValueOfRange)
        {
            this.lowerValueOfRange = lowerValueOfRange;
            this.upperValueOfRange = upperValueOfRange - 1;
            Current = this.upperValueOfRange;
        }

        public void Dispose()
        {
            Reset();
        }

        public bool MoveNext()
        {
            Current--;
            return Current >= upperValueOfRange;
        }

        public void Reset()
        {
            Current = upperValueOfRange;
        }
    }

    public ref struct Enumerator
    {
        private readonly int lowerValueOfRange;
        private readonly int upperValueOfRange;

        public int Current { get; private set; }

        internal Enumerator(int lowerValueOfRange, int upperValueOfRange)
        {
            this.lowerValueOfRange = lowerValueOfRange - 1;
            Current = this.lowerValueOfRange;
            this.upperValueOfRange = upperValueOfRange;
        }

        public void Dispose()
        {
            Reset();
        }

        public bool MoveNext()
        {
            Current++;
            return Current < upperValueOfRange;
        }

        public void Reset()
        {
            Current = lowerValueOfRange;
        }
    }
}
