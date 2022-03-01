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
        public static void AddRange(this IVisualizationSlides self, IAlgorithm algorithm, IEnumerable<IVertex> range)
        {
            range.ForEach(item => self.Add(algorithm, item));
        }

        public static void RemoveRange(this AlgorithmVertices self, IAlgorithm algorithm, IEnumerable<IVertex> range)
        {
            range.ForEach(item => self.Remove(algorithm, item));
        }
    }
}