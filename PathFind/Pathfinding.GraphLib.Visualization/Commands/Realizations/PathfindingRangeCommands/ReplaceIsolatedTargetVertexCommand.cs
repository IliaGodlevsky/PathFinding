using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Visualization.Commands.Abstractions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(7)]
    internal sealed class ReplaceIsolatedTargetVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public ReplaceIsolatedTargetVertexCommand(VisualPathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            Target.VisualizeAsRegular();
            pathfindingRange.Target = vertex;
            Target.VisualizeAsTarget();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return pathfindingRange.Target.IsIsolated()
                && pathfindingRange.Target != null
                && pathfindingRange.CanBeEndPoint(vertex);
        }
    }
}