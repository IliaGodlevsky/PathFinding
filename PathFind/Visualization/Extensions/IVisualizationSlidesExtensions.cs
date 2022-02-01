using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Visualization.Abstractions;
using Visualization.Interfaces;

namespace Visualization.Extensions
{
    internal static class IVisualizationSlidesExtensions
    {
        /// <summary>
        /// Adds a  <<paramref name="range"/> of vertices to be stored
        /// by a particular <paramref name="algorithm"/>
        /// </summary>
        /// <param name="self"></param>
        /// <param name="algorithm"></param>
        /// <param name="range"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange(this IVisualizationSlides self, IAlgorithm algorithm, IEnumerable<IVertex> range)
        {
            range.ForEach(item => self.Add(algorithm, item));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveRange(this AlgorithmProcessSlides self, IAlgorithm algorithm, IEnumerable<IVertex> range)
        {
            range.ForEach(item => self.Remove(algorithm, item));
        }
    }
}