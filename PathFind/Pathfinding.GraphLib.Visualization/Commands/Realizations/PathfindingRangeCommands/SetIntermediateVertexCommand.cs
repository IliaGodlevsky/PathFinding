using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Visualization.Commands.Abstractions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Primitives.Attributes;
using System.Linq;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(9)]
    internal sealed class SetIntermediateVertexCommand<TVertex> : PathfindingRangeIntermediateVertexCommand<TVertex>, IUndo
        where TVertex : IVertex, IVisualizable
    {
        private readonly IExecutable<TVertex> undoCommand;

        public SetIntermediateVertexCommand(PathfindingRangeAdapter<TVertex> pathifindingRange)
            : base(pathifindingRange)
        {
            undoCommand = new UnsetIntermediateVertexCommand<TVertex>(adapter);
        }

        public override void Execute(TVertex vertex)
        {
            IntermediateVertices.Add(vertex);
            vertex.VisualizeAsIntermediate();
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