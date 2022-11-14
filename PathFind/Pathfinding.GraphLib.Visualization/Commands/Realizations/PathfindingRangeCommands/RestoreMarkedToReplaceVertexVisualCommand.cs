using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Visualization.Commands.Abstractions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(2)]
    internal sealed class RestoreMarkedToReplaceVertexVisualCommand<TVertex> : PathfindingRangeIntermediateVertexCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public RestoreMarkedToReplaceVertexVisualCommand(PathfindingRangeAdapter<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            vertex.VisualizeAsIntermediate();
            vertex.VisualizeAsMarkedToReplaceIntermediate();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return IsMarkedToReplace(vertex);
        }
    }
}