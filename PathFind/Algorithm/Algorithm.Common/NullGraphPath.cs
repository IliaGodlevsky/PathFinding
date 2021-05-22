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
        public IEnumerable<IVertex> Path => new NullVertex[] { new NullVertex() };

        public double PathCost => default;
    }
}
