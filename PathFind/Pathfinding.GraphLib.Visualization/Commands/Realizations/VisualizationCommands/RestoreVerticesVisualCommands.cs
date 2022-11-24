using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Adapter;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
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
            yield return new RestoreIntermediateVertexVisualCommand<TVertex>(adapter);
            yield return new RestoreMarkedToReplaceVertexVisualCommand<TVertex>(adapter);
            yield return new RestoreSourceVisualCommand<TVertex>(adapter);
            yield return new RestoreTargetVisualCommand<TVertex>(adapter);
        }
    }
}
