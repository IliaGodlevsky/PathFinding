using System.Collections;
using System.Collections.Generic;

namespace Common.ReadOnly
{
    public sealed class ReadOnlyList<T> : IReadOnlyList<T>
    {
        private readonly IList<T> list;

        public T this[int index] => list[index];

        public int Count => list.Count;

        public ReadOnlyList(IList<T> collection)
        {
            list = collection;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
