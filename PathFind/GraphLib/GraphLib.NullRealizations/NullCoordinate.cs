using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GraphLib.NullRealizations
{
    [Null]
    [DebuggerDisplay("Null")]
    public sealed class NullCoordinate : Singleton<NullCoordinate, ICoordinate>, ICoordinate
    {
        public int Count => 0;

        public int this[int index] => 0;

        private NullCoordinate()
        {

        }

        public override bool Equals(object pos)
        {
            return pos is NullCoordinate;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(ICoordinate other)
        {
            return other is NullCoordinate;
        }

        public IEnumerator<int> GetEnumerator() => Enumerable.Empty<int>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
