using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Pathfinding.GraphLib.Core.Realizations.Range;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(1)]
    internal sealed class RestoreTargetVisual<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public RestoreTargetVisual(PathfindingRange<TVertex> rangeAdapter)
            : base(rangeAdapter)
        {
        }

        public override void Execute(TVertex vertex)
        {
            vertex.VisualizeAsTarget();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return vertex.Equals(pathfindingRange.Target);
        }
    }
}