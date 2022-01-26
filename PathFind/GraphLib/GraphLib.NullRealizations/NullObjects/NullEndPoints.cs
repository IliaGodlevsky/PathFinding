using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System;
using System.Collections.Generic;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    public sealed class NullEndPoints : Singleton<NullEndPoints>, IEndPoints
    {
        public IVertex Target => NullVertex.Instance;

        public IVertex Source => NullVertex.Instance;

        public IEnumerable<IVertex> EndPoints { get; }

        public bool IsEndPoint(IVertex vertex) => false;

        private NullEndPoints() => EndPoints = NullVertex.GetMany(2);
    }
}
