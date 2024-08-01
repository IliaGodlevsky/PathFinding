using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using Shared.Primitives.Single;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Algorithms.GraphPaths
{
    [DebuggerDisplay("Null")]
    public sealed class NullGraphPath : Singleton<NullGraphPath, IGraphPath>, IGraphPath
    {
        public double Cost => default;

        public int Count => default;

        private NullGraphPath()
        {

        }

        public IEnumerator<ICoordinate> GetEnumerator()
        {
            return Enumerable.Empty<ICoordinate>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
