using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.PathfindingRangeCommands
{
    [Order(2)]
    internal sealed class ExcludeIntermediateVertex<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public ExcludeIntermediateVertex(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            //if (MarkedToRemoveIntermediates.Contains(vertex))
            //{
            //    MarkedToRemoveIntermediates.Remove(vertex);
            //}
            IntermediateVertices.Remove(vertex);
        }

        public override bool CanExecute(TVertex vertex)
        {
            return IntermediateVertices.Contains(vertex);
        }
    }
}