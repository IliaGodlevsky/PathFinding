using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Realizations.PathfindingRangeCommands
{
    [Order(0)]
    internal sealed class RemoveMarkToReplaceIntermediateVertexCommand<TVertex> : PathfindingRangeIntermediateVertexCommand<TVertex>
        where TVertex : IVertex
    {
        public RemoveMarkToReplaceIntermediateVertexCommand(PathfindingRange<TVertex> endPoints)
            : base(endPoints)
        {

        }

        public override void Execute(TVertex vertex)
        {
            MarkedToRemoveIntermediates.Remove(vertex);
        }

        public override bool CanExecute(TVertex vertex)
        {
            return IsMarkedToReplace(vertex);
        }
    }
}
