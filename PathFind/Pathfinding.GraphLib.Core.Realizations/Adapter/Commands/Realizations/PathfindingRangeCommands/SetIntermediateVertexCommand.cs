using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Primitives.Attributes;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Realizations.PathfindingRangeCommands
{
    [Order(9)]
    internal sealed class SetIntermediateVertexCommand<TVertex> : PathfindingRangeIntermediateVertexCommand<TVertex>, IUndo
        where TVertex : IVertex
    {
        private readonly IExecutable<TVertex> undoCommand;

        public SetIntermediateVertexCommand(PathfindingRange<TVertex> pathifindingRange)
            : base(pathifindingRange)
        {
            undoCommand = new UnsetIntermediateVertexCommand<TVertex>(pathifindingRange);
        }

        public override void Execute(TVertex vertex)
        {
            IntermediateVertices.Add(vertex);
        }

        public override bool CanExecute(TVertex vertex)
        {
            return adapter.HasSourceAndTargetSet()
                && adapter.CanBeInRange(vertex);
        }

        public void Undo()
        {
            undoCommand.Execute(IntermediateVertices.ToArray());
        }
    }
}