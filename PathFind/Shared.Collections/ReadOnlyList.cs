using System;
using System.Collections;
using System.Collections.Generic;

namespace Shared.Collections
{
    public sealed class ReadOnlyList<T> : IReadOnlyList<T>
    {
        public static readonly ReadOnlyList<T> Empty
            = new ReadOnlyList<T>(Array.Empty<T>());

        private readonly IList<T> list;

        public T this[int index] => list[index];

        public int Count => list.Count;

        public ReadOnlyList(IList<T> collection)
        {
            list = collection;
        }

        public ReadOnlyList(params T[] items)
            : this((IList<T>)items)
        {

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
