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
        public IVertex[] Path { get; }

        public int Length { get; }

        public double Cost { get; }

        public CombinedGraphPath(IGraphPath first,
            IGraphPath second, params IGraphPath[] other)
        {
            var paths = other.Prepend(second).Prepend(first);
            Path = paths.SelectMany(x => x.Path.Reverse()).Reverse().ToArray();
            Length = paths.Select(x => x.Length).Sum();
            Cost = paths.Select(x => x.Cost).Sum();
        }
    }
}