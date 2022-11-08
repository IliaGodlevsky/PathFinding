using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Visualization.Commands.Abstractions;
using Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.VisualizationCommands
{
    internal sealed class RestoreVerticesVisualCommands<TVertex> : PathfindingRangeVisualizationCommands<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public RestoreVerticesVisualCommands(VisualPathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {

        }

        protected override IEnumerable<IVisualizationCommand<TVertex>> GetCommands()
        {
            yield return new RestoreIntermediateVertexVisualCommand<TVertex>(pathfindingRange);
            yield return new RestoreMarkedToReplaceVertexVisualCommand<TVertex>(pathfindingRange);
            yield return new RestoreSourceVisualCommand<TVertex>(pathfindingRange);
            yield return new RestoreTargetVisualCommand<TVertex>(pathfindingRange);
        }
    }
}
