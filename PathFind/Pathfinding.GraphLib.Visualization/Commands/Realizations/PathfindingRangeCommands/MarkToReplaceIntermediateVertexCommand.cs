using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Visualization.Commands.Abstractions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using Shared.Primitives.Attributes;
using System.Linq;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(1)]
    internal sealed class MarkToReplaceIntermediateVertexCommand<TVertex> : PathfindingRangeIntermediateVertexCommand<TVertex>, IUndo
        where TVertex : IVertex, IVisualizable
    {
        private readonly IExecutable<TVertex> undoCommand;

        public MarkToReplaceIntermediateVertexCommand(PathfindingRangeAdapter<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
            undoCommand = new RemoveMarkToReplaceIntermediateVertexCommand<TVertex>(pathfindingRange);
        }

        public override void Execute(TVertex vertex)
        {
            MarkedToRemoveIntermediates.Add(vertex);
            vertex.VisualizeAsMarkedToReplaceIntermediate();
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
