using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Adapter;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
using Shared.Primitives.Attributes;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Realizations.PathfindingRangeCommands
{
    [Order(3)]
    internal sealed class ReplaceIntermediateIsolatedVertexCommand<TVertex> : PathfindingRangeIntermediateVertexCommand<TVertex>
        where TVertex : IVertex
    {
        public ReplaceIntermediateIsolatedVertexCommand(PathfindingRange<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            var isolated = IntermediateVertices.First(v => v.IsIsolated());
            int isolatedIndex = IntermediateVertices.IndexOf(isolated);
            MarkedToRemoveIntermediates.Remove(isolated);
            IntermediateVertices.Remove(isolated);
            IntermediateVertices.Insert(isolatedIndex, vertex);
        }

        public override bool CanExecute(TVertex vertex)
        {
            return adapter.HasSourceAndTargetSet()
                && HasIsolatedIntermediates
                && adapter.CanBeInRange(vertex);
        }
    }
}