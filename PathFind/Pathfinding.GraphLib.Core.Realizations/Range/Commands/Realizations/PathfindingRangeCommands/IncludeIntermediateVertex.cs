using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Primitives.Attributes;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.PathfindingRangeCommands
{
    [Order(8)]
    internal sealed class IncludeIntermediateVertex<TVertex> : PathfindingRangeCommand<TVertex>, IUndo
        where TVertex : IVertex
    {
        private readonly IExecutable<TVertex> undoCommand;

        public IncludeIntermediateVertex(PathfindingRange<TVertex> pathifindingRange)
            : base(pathifindingRange)
        {
            undoCommand = new ExcludeIntermediateVertex<TVertex>(pathifindingRange);
        }

        public override void Execute(TVertex vertex)
        {
            IntermediateVertices.Add(vertex);
        }

        public override bool CanExecute(TVertex vertex)
        {
            return pathfindingRange.HasSourceAndTargetSet()
                && pathfindingRange.CanBeInRange(vertex);
        }

        public void Undo()
        {
            undoCommand.Execute(IntermediateVertices.ToArray());
        }
    }
}