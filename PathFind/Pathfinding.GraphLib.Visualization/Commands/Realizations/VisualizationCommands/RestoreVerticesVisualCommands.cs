using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Pathfinding.GraphLib.Core.Realizations.Range;
using Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.VisualizationCommands
{
    internal sealed class RestoreVerticesVisualCommands<TVertex> : PathfindingRangeCommands<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public RestoreVerticesVisualCommands(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {

        }

        protected override IEnumerable<PathfindingRangeCommand<TVertex>> GetCommands()
        {
            yield return new RestoreIntermediateVertexVisual<TVertex>(pathfindingRange);
            yield return new RestoreSourceVisual<TVertex>(pathfindingRange);
            yield return new RestoreTargetVisual<TVertex>(pathfindingRange);
        }
    }
}
