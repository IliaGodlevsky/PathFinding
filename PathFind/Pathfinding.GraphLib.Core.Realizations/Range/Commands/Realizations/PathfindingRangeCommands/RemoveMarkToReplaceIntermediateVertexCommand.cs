using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.PathfindingRangeCommands
{
    [Order(0)]
    internal sealed class RemoveMarkToReplaceIntermediateVertexCommand<TVertex> : ReplaceIntermediatesCommand<TVertex>
        where TVertex : IVertex
    {
        public RemoveMarkToReplaceIntermediateVertexCommand(PathfindingRange<TVertex> pathfindingRange, 
            ReplaceIntermediateVerticesModule<TVertex> module)
            : base(pathfindingRange, module)
        {

        }

        public override void Execute(TVertex vertex)
        {
            ToReplaceIntermediates.Remove(vertex);
        }

        public override bool CanExecute(TVertex vertex)
        {
            return ToReplaceIntermediates.Contains(vertex);
        }
    }
}
