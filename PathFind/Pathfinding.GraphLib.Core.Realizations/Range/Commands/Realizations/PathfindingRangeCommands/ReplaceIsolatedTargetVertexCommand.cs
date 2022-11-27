using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.PathfindingRangeCommands
{
    [Order(7)]
    internal sealed class ReplaceIsolatedTargetVertex<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public ReplaceIsolatedTargetVertex(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            pathfindingRange.Target = vertex;
        }

        public override bool CanExecute(TVertex vertex)
        {
            return pathfindingRange.Target != null
                && pathfindingRange.Target.IsIsolated()
                && pathfindingRange.CanBeInRange(vertex);
        }
    }
}