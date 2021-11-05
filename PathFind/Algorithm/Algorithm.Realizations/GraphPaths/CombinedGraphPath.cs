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

        public int PathLength { get; }

        public double PathCost { get; }

        public CombinedGraphPath(IGraphPath first,
            IGraphPath second, params IGraphPath[] other)
        {
            var paths = other.Prepend(second).Prepend(first);
            Path = paths.SelectMany(x => x.Path.Reverse()).Reverse().ToArray();
            PathLength = paths.Select(x => x.PathLength).Sum();
            PathCost = paths.Select(x => x.PathCost).Sum();
        }
    }
}