using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.AlgorithmLib.Core.Realizations.GraphPaths
{
    public sealed class CompositeGraphPath : IGraphPath
    {
        private IReadOnlyCollection<ICoordinate> Path { get; }

        public int Count { get; }

        public double Cost { get; }

        public CompositeGraphPath(IGraphPath path, IGraphPath second, params IGraphPath[] paths)
        {
            var composite = paths.Prepend(second).Prepend(path).ToArray();
            Path = composite.SelectMany(p => p.Reverse()).Reverse().ToArray();
            Count = Path.Count == 0 ? 0 : composite.Sum(p => p.Count);
            Cost = Path.Count == 0 ? 0 : composite.Sum(p => p.Cost);
        }

        public IEnumerator<ICoordinate> GetEnumerator()
        {
            return Path.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}