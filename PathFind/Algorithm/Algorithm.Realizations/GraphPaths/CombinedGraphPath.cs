using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Realizations.GraphPaths
{
    /// <summary>
    /// A class, that merges two or more graph 
    /// pathes into one
    /// </summary>
    public sealed class CombinedGraphPath : IGraphPath
    {
        /// <summary>
        /// A path that includes all combined paths
        /// </summary>
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