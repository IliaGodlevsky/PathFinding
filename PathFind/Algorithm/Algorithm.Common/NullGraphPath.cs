using Algorithm.Interfaces;
using Common.Attributes;
using GraphLib.Common.NullObjects;
using GraphLib.Interface;
using System.Collections.Generic;

namespace Algorithm.Common
{
    [Default]
    public sealed class NullGraphPath : IGraphPath
    {
        public IEnumerable<IVertex> Path => new DefaultVertex[] { };
    }
}
