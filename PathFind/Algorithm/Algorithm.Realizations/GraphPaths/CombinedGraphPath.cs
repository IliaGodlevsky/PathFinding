using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Realizations.GraphPaths
{
    public sealed class CombinedGraphPath : IGraphPath
    {
        public IReadOnlyList<IVertex> Path { get; }

        public int Length { get; }

        public double Cost { get; }

        public CombinedGraphPath(params IGraphPath[] paths)
        {
            Path = paths.SelectMany(x => x.Path.Reverse()).Reverse().ToArray();
            Length = paths.Sum(x => x.Length);
            Cost = paths.Sum(x => x.Cost);
        }
    }
}