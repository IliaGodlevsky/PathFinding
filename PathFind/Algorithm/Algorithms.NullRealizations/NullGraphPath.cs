using Algorithm.Interfaces;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using NullObject.Attributes;
using SingletonLib;
using System.Collections.Generic;
using System.Diagnostics;

namespace Algorithm.NullRealizations
{
    [Null]
    [DebuggerDisplay("Null")]
    public sealed class NullGraphPath : Singleton<NullGraphPath, IGraphPath>, IGraphPath
    {
        public IReadOnlyList<IVertex> Path => NullVertex.GetMany(0);

        public double Cost => default;

        public int Length => default;

        private NullGraphPath()
        {

        }
    }
}
