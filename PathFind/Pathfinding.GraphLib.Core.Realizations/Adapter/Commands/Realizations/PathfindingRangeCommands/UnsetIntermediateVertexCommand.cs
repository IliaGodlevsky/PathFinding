using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Realizations.PathfindingRangeCommands
{
    [Order(2)]
    internal sealed class UnsetIntermediateVertexCommand<TVertex> : PathfindingRangeIntermediateVertexCommand<TVertex>
        where TVertex : IVertex
    {
        public UnsetIntermediateVertexCommand(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            if (IsMarkedToReplace(vertex))
            {
                MarkedToRemoveIntermediates.Remove(vertex);
            }
            IntermediateVertices.Remove(vertex);
        }

        public override bool CanExecute(TVertex vertex)
        {
            return IsIntermediate(vertex);
        }
    }
}