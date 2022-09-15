using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace GraphLib.NullRealizations
{
    [Null]
    [DebuggerDisplay("Null")]
    public sealed class NullNeighborhood : Singleton<NullNeighborhood, INeighborhood>, INeighborhood
    {
        public int Count => 0;

        private NullNeighborhood()
        {

        }

        public IEnumerator<ICoordinate> GetEnumerator()
        {
            return NullCoordinate.GetMany(0).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}