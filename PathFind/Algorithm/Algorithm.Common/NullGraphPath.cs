using Algorithm.Interfaces;
using GraphLib.Common.NullObjects;
using GraphLib.Interface;
using System.Collections.Generic;

namespace Algorithm.Common
{
    public sealed class NullGraphPath : IGraphPath
    {
        public IEnumerable<IVertex> Path => new DefaultVertex[] { };
    }
}
