using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Realizations.GraphPaths
{
    public sealed class CompositeGraphPath : IGraphPath
    {
        private IEnumerable<IVertex> Path { get; }

        public int Count { get; }

        public double Cost { get; }

        public CompositeGraphPath(IGraphPath path, IGraphPath second, params IGraphPath[] paths)
        {
            var composite = paths.Prepend(second).Prepend(path).ToArray();
            Path = composite.SelectMany(p => p.Reverse()).Reverse().ToArray();
            Count = composite.Sum(p => p.Count);
            Cost = composite.Sum(p => p.Cost);
        }

        public IEnumerator<IVertex> GetEnumerator() => Path.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}