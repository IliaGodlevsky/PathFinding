using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using Shared.Primitives.Attributes;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.PathfindingRangeCommands
{
    [Order(1)]
    internal sealed class MarkToReplaceIntermediateVertexCommand<TVertex> : ReplaceIntermediatesCommand<TVertex>, IUndo
        where TVertex : IVertex
    {
        private readonly IExecutable<TVertex> undoCommand;

        public MarkToReplaceIntermediateVertexCommand(PathfindingRange<TVertex> pathfindingRange, 
            ReplaceIntermediateVerticesModule<TVertex> module)
            : base(pathfindingRange, module)
        {
            undoCommand = new RemoveMarkToReplaceIntermediateVertexCommand<TVertex>(pathfindingRange, module);
        }

        public override void Execute(TVertex vertex)
        {
            ToReplaceIntermediates.Add(vertex);
        }

        public override bool CanExecute(TVertex vertex)
        {
            return !vertex.IsOneOf(pathfindingRange.Source, pathfindingRange.Target)
                && IntermediateVertices.Contains(vertex)
                && !ToReplaceIntermediates.Contains(vertex);
        }

        public void Undo()
        {
            undoCommand.Execute(ToReplaceIntermediates.ToArray());
        }
    }
}
