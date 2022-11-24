using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
using Shared.Executable;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Realizations.PathfindingRangeCommands
{
    [Order(6)]
    internal sealed class SetTargetVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>, IUndo
        where TVertex : IVertex
    {
        private readonly IExecutable<TVertex> undoCommand;

        public SetTargetVertexCommand(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
            undoCommand = new UnsetTargetVertexCommand<TVertex>(pathfindingRange);
        }

        public override void Execute(TVertex vertex)
        {
            adapter.Target = vertex;
        }

        public override bool CanExecute(TVertex vertex)
        {
            return adapter.Source != null
                && adapter.Target == null
                && adapter.CanBeInRange(vertex);
        }

        public void Undo()
        {
            undoCommand.Execute(Target);
        }
    }
}
