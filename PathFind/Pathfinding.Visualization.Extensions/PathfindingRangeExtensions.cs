using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Visualization.Extensions
{
    public static class PathfindingRangeExtensions
    {
        public static void RestoreVerticesVisualState<TVertex>(this IPathfindingRange<TVertex> range)
            where TVertex : IVertex, IVisualizable
        {
            var commands = new List<IVisualCommand<TVertex>>()
            {
                new RestoreTransitVerticesVisual<TVertex>(),
                new RestoreSourceVisual<TVertex>(),
                new RestoreTargetVisual<TVertex>()
            };

            foreach (var vertex in range)
            {
                commands.FirstOrDefault(command => command.CanExecute(range, vertex))
                    ?.Execute(vertex);
            }
        }
    }
}
