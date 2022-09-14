using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using Visualization.Abstractions;
using Visualization.Interfaces;

namespace Visualization.Extensions
{
    internal static class IVisualizationSlidesExtensions
    {
        public static void AddRange<TAdd>(this IVisualizationSlides<TAdd> self, IAlgorithm<IGraphPath> algorithm, IEnumerable<TAdd> range)
        {
            range.ForEach(item => self.Add(algorithm, item));
        }

        public static void RemoveRange(this AlgorithmVertices self, IAlgorithm<IGraphPath> algorithm, IEnumerable<IVertex> range)
        {
            range.ForEach(item => self.Remove(algorithm, item));
        }
    }
}