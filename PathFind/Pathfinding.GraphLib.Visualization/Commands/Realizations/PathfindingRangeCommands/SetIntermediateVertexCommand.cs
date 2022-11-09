using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
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
    [Order(9)]
    internal sealed class SetIntermediateVertexCommand<TVertex> : PathfindingRangeIntermediateVertexCommand<TVertex>, IUndo
        where TVertex : IVertex, IVisualizable
    {
        private readonly IExecutable<TVertex> undoCommand;

        public SetIntermediateVertexCommand(VisualPathfindingRange<TVertex> pathifindingRange)
            : base(pathifindingRange)
        {
            undoCommand = new UnsetIntermediateVertexCommand<TVertex>(pathfindingRange);
        }

        public override void Execute(TVertex vertex)
        {
            IntermediateVertices.Add(vertex);
            vertex.VisualizeAsIntermediate();
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