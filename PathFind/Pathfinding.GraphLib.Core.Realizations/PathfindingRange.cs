using Pathfinding.GraphLib.Core.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations
{
    public sealed class PathfindingRange<TVertex> : IPathfindingRange<TVertex>
        where TVertex : IVertex
    {
        public TVertex Source { get; set; }

        public TVertex Target { get; set; }

        public IList<TVertex> Transit { get; } = new List<TVertex>();

        public IEnumerator<TVertex> GetEnumerator()
        {
            return Transit.Append(Target)
                .Prepend(Source)
                .Where(vertex => vertex is not null)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}