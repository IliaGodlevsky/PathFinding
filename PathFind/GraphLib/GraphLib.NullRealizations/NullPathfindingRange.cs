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
    public sealed class NullPathfindingRange : Singleton<NullPathfindingRange, IPathfindingRange>, IPathfindingRange
    {
        public IVertex Target => NullVertex.Interface;

        public IVertex Source => NullVertex.Interface;

        private NullPathfindingRange() { }

        public IEnumerator<IVertex> GetEnumerator()
        {
            yield return NullVertex.Instance;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
