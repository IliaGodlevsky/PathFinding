using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Shared.Primitives.Attributes;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.PathfindingRangeCommands
{
    [Order(3)]
    internal sealed class ReplaceIntermediateIsolatedVertex<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public ReplaceIntermediateIsolatedVertex(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            var isolated = IntermediateVertices.First(IsIsolated);
            int isolatedIndex = IntermediateVertices.IndexOf(isolated);
            IntermediateVertices.Remove(isolated);
            IntermediateVertices.Insert(isolatedIndex, vertex);
        }

        public override bool CanExecute(TVertex vertex)
        {
            return IntermediateVertices.Count > 0
                && pathfindingRange.HasSourceAndTargetSet()
                && IntermediateVertices.Any(IsIsolated)
                && pathfindingRange.CanBeInRange(vertex);
        }

        private static bool IsIsolated(TVertex vertex)
        {
            return vertex.IsIsolated();
        }
    }
}