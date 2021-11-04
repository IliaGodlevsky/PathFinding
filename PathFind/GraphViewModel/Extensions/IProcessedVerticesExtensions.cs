using Algorithm.Interfaces;
using GraphLib.Interfaces;
using GraphViewModel.Interfaces;
using GraphViewModel.Visualizations;
using System.Collections.Generic;

namespace GraphViewModel.Extensions
{
    internal static class IProcessedVerticesExtensions
    {
        public static void AddRange(this IProcessedVertices self, IAlgorithm algorithm, IEnumerable<IVertex> range)
        {
            foreach (var item in range)
            {
                self.Add(algorithm, item);
            }
        }

        public static void RemoveRange(this AlgorithmVertices self, IAlgorithm algorithm, IEnumerable<IVertex> range)
        {
            foreach (var item in range)
            {
                self.Remove(algorithm, item);
            }
        }
    }
}