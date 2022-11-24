using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Adapter;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(0)]
    internal sealed class RestoreSourceVisualCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public RestoreSourceVisualCommand(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            vertex.VisualizeAsSource();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return vertex.Equals(adapter.Source);
        }
    }
}