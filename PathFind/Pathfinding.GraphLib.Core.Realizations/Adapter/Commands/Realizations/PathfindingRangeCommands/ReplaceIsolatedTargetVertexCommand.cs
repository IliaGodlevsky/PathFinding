using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Realizations.PathfindingRangeCommands
{
    [Order(7)]
    internal sealed class ReplaceIsolatedTargetVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public ReplaceIsolatedTargetVertexCommand(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            adapter.Target = vertex;
        }

        public override bool CanExecute(TVertex vertex)
        {
            return adapter.Target.IsIsolated()
                && adapter.Target != null
                && adapter.CanBeInRange(vertex);
        }
    }
}