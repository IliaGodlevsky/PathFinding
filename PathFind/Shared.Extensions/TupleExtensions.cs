using System;

namespace Shared.Extensions
{
    public static class TupleExtensions
    {
        public ref struct Enumerator
        {
            private readonly int start;
            private readonly int end;

            public int Current { get; private set; }

            internal Enumerator(int start, int end)
            {
                if (start > end)
                {
                    int temp = end;
                    end = start;
                    start = temp;
                }
                this.start = start;
                this.end = end - 1;
                Current = start - 1;
            }

            public bool MoveNext() => Current++ < end;

            public void Reset() => Current = start - 1;

            public void Dispose() => Reset();           
        }

        public static Enumerator GetEnumerator(this(int start, int end) tuple)
        {
            return new(tuple.start, tuple.end);
        }
    }
}
