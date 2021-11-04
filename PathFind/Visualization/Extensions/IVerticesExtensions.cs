using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System.Collections.Generic;
using Visualization.Abstractions;
using Visualization.Interfaces;

namespace Visualization.Extensions
{
    internal static class IVerticesExtensions
    {
        public static void AddRange(this IVertices self, IAlgorithm algorithm, IEnumerable<IVertex> range)
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