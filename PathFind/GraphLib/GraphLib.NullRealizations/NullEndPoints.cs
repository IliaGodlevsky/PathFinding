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
    public sealed class NullEndPoints : Singleton<NullEndPoints, IEndPoints>, IEndPoints
    {
        public IVertex Target => NullVertex.Instance;

        public IVertex Source => NullVertex.Instance;

        private NullEndPoints() { }

        public IEnumerator<IVertex> GetEnumerator()
        {
            yield return NullVertex.Instance;
            yield return NullVertex.Instance;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
