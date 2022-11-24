using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Adapter;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(1)]
    internal sealed class RestoreTargetVisualCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public RestoreTargetVisualCommand(PathfindingRange<TVertex> rangeAdapter)
            : base(rangeAdapter)
        {
        }

        public override void Execute(TVertex vertex)
        {
            vertex.VisualizeAsTarget();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return vertex.Equals(adapter.Target);
        }
    }
}