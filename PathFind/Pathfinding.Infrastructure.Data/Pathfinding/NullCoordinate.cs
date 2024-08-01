using Pathfinding.Domain.Interface;
using Shared.Primitives.Single;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pathfinding.Infrastructure.Data.Pathfinding
{
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

        public IEnumerator<int> GetEnumerator()
        {
            return Enumerable.Empty<int>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
