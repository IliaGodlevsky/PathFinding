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
        public CombinedGraphPath(IGraphPath first,
            IGraphPath second, params IGraphPath[] other)
        {
            var paths = other.Prepend(second).Prepend(first);
            Path = GetGraphPath(paths);
            PathLength = GetPathLength(paths);
            PathCost = GetPathCost(paths);
        }

        public IVertex[] Path { get; }

        public int PathLength { get; }

        public double PathCost { get; }

        private IVertex[] GetGraphPath(IEnumerable<IGraphPath> paths)
        {
            return paths
                .Select(x => x.Path.Reverse())
                .Aggregate((x, y) => x.Concat(y))
                .Reverse()
                .ToArray();
        }

        private double GetPathCost(IEnumerable<IGraphPath> paths)
        {
            return paths
                .Select(x => x.PathCost)
                .Aggregate((x, y) => x + y);
        }

        private int GetPathLength(IEnumerable<IGraphPath> paths)
        {
            return paths
                .Select(x => x.PathLength)
                .Aggregate((x, y) => x + y);
        }
    }
}