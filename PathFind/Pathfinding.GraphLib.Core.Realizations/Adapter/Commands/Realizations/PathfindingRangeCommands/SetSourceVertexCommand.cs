using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
using Shared.Executable;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Realizations.PathfindingRangeCommands
{
    [Order(4)]
    internal sealed class SetSourceVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>, IUndo
        where TVertex : IVertex
    {
        private readonly IExecutable<TVertex> unsetCommand;

        public SetSourceVertexCommand(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
            unsetCommand = new UnsetSourceVertexCommand<TVertex>(pathfindingRange);
        }

        public override void Execute(TVertex vertex)
        {
            adapter.Source = vertex;
        }

        public override bool CanExecute(TVertex vertex)
        {
            return adapter.Source == null
                && adapter.CanBeInRange(vertex);
        }

        public void Undo()
        {
            unsetCommand.Execute(Source);
        }
    }
}