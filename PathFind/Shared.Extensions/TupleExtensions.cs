using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Shared.Extensions
{
    public static class TupleExtensions
    {
        public ref struct Enumerator
        {
            private readonly int start;
            private readonly int end;
            private int current;

            internal Enumerator(int start, int end)
            {
                if (start > end)
                {
                    int temp = end;
                    end = start;
                    start = temp;
                }
                this.start = start;
                this.end = end;
                current = start - 1;
            }

            public bool MoveNext()
            {
                if (current < end - 1)
                {
                    current++;
                    return true;
                }
                return false;
            }

            public int Current => current;

            public void Reset()
            {
                current = start - 1;
            }

            public void Dispose()
            {
                Reset();
            }
        }

        public static Enumerator GetEnumerator(this(int start, int end) tuple)
        {
            return new(tuple.start, tuple.end);
        }
    }
}
