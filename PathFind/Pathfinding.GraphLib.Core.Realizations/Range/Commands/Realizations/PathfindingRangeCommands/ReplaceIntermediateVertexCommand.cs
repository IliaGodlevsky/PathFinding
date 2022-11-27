using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Shared.Primitives.Attributes;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.PathfindingRangeCommands
{
    [Order(2)]
    internal sealed class ReplaceIntermediateVertex<TVertex> : ReplaceIntermediatesCommand<TVertex>
        where TVertex : IVertex
    {
        public ReplaceIntermediateVertex(PathfindingRange<TVertex> pathfindingRange, ReplaceIntermediateVerticesModule<TVertex> module)
            : base(pathfindingRange, module)
        {

        }

        public override void Execute(TVertex vertex)
        {
            var toRemove = ToReplaceIntermediates.First();
            ToReplaceIntermediates.Remove(toRemove);
            int toReplaceIndex = IntermediateVertices.IndexOf(toRemove);
            while (toReplaceIndex == -1 && ToReplaceIntermediates.Count > 0)
            {
                toRemove = ToReplaceIntermediates.First();
                ToReplaceIntermediates.Remove(toRemove);
                toReplaceIndex = IntermediateVertices.IndexOf(toRemove);
            }
            if (toReplaceIndex != -1)
            {
                IntermediateVertices.Remove(toRemove);
                IntermediateVertices.Insert(toReplaceIndex, vertex);
            }
        }

        public override bool CanExecute(TVertex vertex)
        {
            return ToReplaceIntermediates.Count > 0
                && pathfindingRange.CanBeInRange(vertex);
        }
    }
}
