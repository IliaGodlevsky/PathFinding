using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Adapter;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(3)]
    internal sealed class RestoreIntermediateVertexVisualCommand<TVertex> : PathfindingRangeIntermediateVertexCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public RestoreIntermediateVertexVisualCommand(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            vertex.VisualizeAsIntermediate();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return IsIntermediate(vertex);
        }
    }
}