using Pathfinding.GraphLib.Core.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations
{
    public sealed class PathfindingRange : IPathfindingRange
    {
        private IEnumerable<IVertex> IntermediateVertices { get; }

        public IVertex Target { get; }

        public IVertex Source { get; }

        public PathfindingRange(IVertex source, IVertex target, IEnumerable<IVertex> intermediate)
        {
            Source = source;
            Target = target;
            IntermediateVertices = intermediate;
        }

        public IEnumerator<IVertex> GetEnumerator()
        {
            return IntermediateVertices
                .Append(Target)
                .Prepend(Source)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
