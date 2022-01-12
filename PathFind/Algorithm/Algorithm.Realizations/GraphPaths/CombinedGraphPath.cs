using Algorithm.Interfaces;
using GraphLib.Interfaces;
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
        public IVertex[] Path { get; }

        public int Length { get; }

        public double Cost { get; }

        public CombinedGraphPath(params IGraphPath[] paths)
        {
            Path = paths.SelectMany(x => x.Path.Reverse()).Reverse().ToArray();
            Length = paths.Select(x => x.Length).Sum();
            Cost = paths.Select(x => x.Cost).Sum();
        }
    }
}