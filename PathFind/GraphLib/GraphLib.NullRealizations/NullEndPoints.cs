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

        private IEnumerable<IVertex> EndPoints { get; }

        private NullEndPoints() => EndPoints = NullVertex.GetMany(2);

        public IEnumerator<IVertex> GetEnumerator()
        {
            return EndPoints.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
