using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.Single;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pathfinding.AlgorithmLib.Core.NullObjects
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
            return Enumerable
                .Empty<ICoordinate>()
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
