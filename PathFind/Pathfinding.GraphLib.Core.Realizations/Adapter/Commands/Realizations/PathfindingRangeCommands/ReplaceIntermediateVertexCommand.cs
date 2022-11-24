using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
using Shared.Primitives.Attributes;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Realizations.PathfindingRangeCommands
{
    [Order(8)]
    internal sealed class ReplaceIntermediateVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public ReplaceIntermediateVertexCommand(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {

        }

        public override void Execute(TVertex vertex)
        {
            var toRemove = MarkedToRemoveIntermediates.First();
            MarkedToRemoveIntermediates.Remove(toRemove);
            int toReplaceIndex = IntermediateVertices.IndexOf(toRemove);
            IntermediateVertices.Remove(toRemove);
            IntermediateVertices.Insert(toReplaceIndex, vertex);
        }

        public override bool CanExecute(TVertex vertex)
        {
            return MarkedToRemoveIntermediates.Count > 0
                && adapter.CanBeInRange(vertex);
        }
    }
}
