using Algorithm.Interfaces;
using Common.Attributes;
using GraphLib.Common.NullObjects;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace Algorithm.Common
{
    [Null]
    public sealed class NullGraphPath : IGraphPath
    {
        public IEnumerable<IVertex> Path => new NullVertex[] { };

        public double PathCost => default;
    }
}
