using System;

namespace Common.Extensions
{
    public static class TupleExtensions
    {
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

            public void Dispose() => Reset();

            public bool MoveNext() => ++Current < upperValueOfRange;

            public void Reset() => Current = lowerValueOfRange;
        }

        public static Enumerator GetEnumerator(this (int LowerValueOfRange, int UpperValueOfRange) range)
        {
            if (range.LowerValueOfRange > range.UpperValueOfRange)
            {
                throw new ArgumentOutOfRangeException();
            }
            return new Enumerator(range.LowerValueOfRange, range.UpperValueOfRange);
        }
    }   
}
