using Algorithm.Interfaces;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using NullObject.Attributes;
using System.Collections.Generic;

namespace Algorithm.Common
{
    [Null]
    public sealed class NullGraphPath : IGraphPath
    {
        public NullGraphPath()
        {
            Path = new NullVertex[] { new NullVertex() };
        }

        public IEnumerable<IVertex> Path { get; }

        public double PathCost => default;

        public int PathLength => default;
    }
}
