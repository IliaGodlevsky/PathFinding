using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range;
using Pathfinding.GraphLib.Visualization.Commands.Realizations.VisualizationCommands;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable.Extensions;
using Shared.Extensions;
using System.Linq;

namespace Pathfinding.Visualization.Extensions
{
    public static class PathfindingRangeExtensions
    {
        public static void RestoreVerticesVisualState<TVertex>(this PathfindingRange<TVertex> range)
            where TVertex : IVertex, IVisualizable
        {
            var commands = new RestoreVerticesVisualCommands<TVertex>(range);
            var vertices = range.Cast<TVertex>().ToList();
            vertices.ForEach(commands.Execute);
        }
    }
}
