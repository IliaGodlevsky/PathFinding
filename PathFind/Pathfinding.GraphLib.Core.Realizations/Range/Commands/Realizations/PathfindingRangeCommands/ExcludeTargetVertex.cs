using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.PathfindingRangeCommands
{
    [Order(1)]
    internal sealed class ExcludeTargetVertex<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public ExcludeTargetVertex(PathfindingRange<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            pathfindingRange.Target = default;
        }

        public override bool CanExecute(TVertex vertex)
        {
            return pathfindingRange.Target?.Equals(vertex) == true;
        }
    }
}