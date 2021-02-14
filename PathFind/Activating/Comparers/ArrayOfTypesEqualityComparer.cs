using System;
using System.Collections.Generic;
using System.Linq;

namespace Activating.Comparers
{
    internal class ArrayOfTypesEqualityComparer : IEqualityComparer<Type[]>
    {
        public bool Equals(Type[] x, Type[] y)
        {
            if (!x.Any() && !y.Any())
            {
                return true;
            }

            return x.SequenceEqual(y, new TypeEqualityComparer());
        }

        public int GetHashCode(Type[] obj)
        {
            if (!obj.Any())
            {
                return string.Empty.GetHashCode();
            }

            return obj
                .Select(o => o.FullName.GetHashCode())
                .Aggregate((x, y) => x ^ y);
        }
    }
}
