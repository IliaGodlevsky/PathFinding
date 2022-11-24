using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using Shared.Primitives.Attributes;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Realizations.PathfindingRangeCommands
{
    [Order(1)]
    internal sealed class MarkToReplaceIntermediateVertexCommand<TVertex> : PathfindingRangeIntermediateVertexCommand<TVertex>, IUndo
        where TVertex : IVertex
    {
        private readonly IExecutable<TVertex> undoCommand;

        public MarkToReplaceIntermediateVertexCommand(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
            undoCommand = new RemoveMarkToReplaceIntermediateVertexCommand<TVertex>(pathfindingRange);
        }

        public override void Execute(TVertex vertex)
        {
            MarkedToRemoveIntermediates.Add(vertex);
        }

        public override bool CanExecute(TVertex vertex)
        {
            return !vertex.IsOneOf(adapter.Source, adapter.Target)
                && IsIntermediate(vertex)
                && !IsMarkedToReplace(vertex);
        }

        public void Undo()
        {
            undoCommand.Execute(MarkedToRemoveIntermediates.ToArray());
        }
    }
}
