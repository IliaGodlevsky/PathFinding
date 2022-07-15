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

        public IEnumerable<IVertex> EndPoints { get; }

        private NullEndPoints() => EndPoints = NullVertex.GetMany(2);
    }
}
