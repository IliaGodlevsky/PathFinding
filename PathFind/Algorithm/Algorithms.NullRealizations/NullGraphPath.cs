using Algorithm.Interfaces;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using NullObject.Attributes;
using SingletonLib;
using System.Collections.Generic;
using System.Diagnostics;

namespace Algorithm.NullRealizations
{
    /// <summary>
    /// A class, that represents a null analog for
    /// <see cref="IGraphPath"/>. This class is a singleton
    /// </summary>
    [Null]
    [DebuggerDisplay("Null")]
    public sealed class NullGraphPath : Singleton<NullGraphPath>, IGraphPath
    {
        public IReadOnlyList<IVertex> Path => NullVertex.GetMany(0);

        public double Cost => default;

        public int Length => default;

        private NullGraphPath()
        {

        }
    }
}
