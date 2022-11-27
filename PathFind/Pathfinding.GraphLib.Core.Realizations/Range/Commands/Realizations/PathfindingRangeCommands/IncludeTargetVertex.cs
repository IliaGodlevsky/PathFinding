using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Shared.Executable;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.PathfindingRangeCommands
{
    [Order(6)]
    internal sealed class IncludeTargetVertex<TVertex> : PathfindingRangeCommand<TVertex>, IUndo
        where TVertex : IVertex
    {
        private readonly IExecutable<TVertex> undoCommand;

        public IncludeTargetVertex(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
            undoCommand = new ExcludeTargetVertex<TVertex>(pathfindingRange);
        }

        public override void Execute(TVertex vertex)
        {
            pathfindingRange.Target = vertex;
        }

        public override bool CanExecute(TVertex vertex)
        {
            return pathfindingRange.Source != null
                && pathfindingRange.Target == null
                && pathfindingRange.CanBeInRange(vertex);
        }

        public void Undo()
        {
            undoCommand.Execute(Target);
        }
    }
}
