using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Visualization.Abstractions;
using Pathfinding.AlgorithmLib.Visualization.Interfaces;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Visualization.Extensions
{
    internal static class IVisualizationSlidesExtensions
    {
        public static void AddRange<TAdd>(this IVisualizationSlides<TAdd> self, IAlgorithm<IGraphPath> algorithm, IEnumerable<TAdd> range)
        {
            range.ForEach(item => self.Add(algorithm, item));
        }

        public static void RemoveRange<TVertex>(this PathfindingVertices<TVertex> self, IAlgorithm<IGraphPath> algorithm, IEnumerable<TVertex> range)
            where TVertex : IVertex, IVisualizable
        {
            range.ForEach(item => self.Remove(algorithm, item));
        }
    }
}