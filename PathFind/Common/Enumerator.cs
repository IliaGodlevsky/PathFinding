namespace Common
{
    public ref struct Enumerator
    {
        private readonly int lowerValueOfRange;
        private readonly int upperValueOfRange;

        public int Current { get; private set; }

        public Enumerator(int lowerValueOfRange, int upperValueOfRange)
        {
            this.lowerValueOfRange = lowerValueOfRange - 1;
            Current = this.lowerValueOfRange;
            this.upperValueOfRange = upperValueOfRange;
        }

        public void Dispose() => Reset();

        public bool MoveNext() => ++Current < upperValueOfRange;

        public void Reset() => Current = lowerValueOfRange;
    }
}
