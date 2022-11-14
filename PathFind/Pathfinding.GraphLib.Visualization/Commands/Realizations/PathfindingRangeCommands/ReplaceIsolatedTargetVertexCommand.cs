using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Visualization.Commands.Abstractions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(7)]
    internal sealed class ReplaceIsolatedTargetVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public ReplaceIsolatedTargetVertexCommand(PathfindingRangeAdapter<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            Target.VisualizeAsRegular();
            adapter.Target = vertex;
            Target.VisualizeAsTarget();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return adapter.Target.IsIsolated()
                && adapter.Target != null
                && adapter.CanBeInRange(vertex);
        }
    }
}