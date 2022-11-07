using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.Visualization.Core.Abstractions.Commands.Abstractions
{
    internal abstract class PathfindingRangeCommand<TVertex> : IVisualizationCommand
        where TVertex : IVertex, IVisualizable
    {
        protected TVertex Source => pathfindingRange.Source;

        protected TVertex Target => pathfindingRange.Target;

        protected readonly VisualPathfindingRange<TVertex> pathfindingRange;

        protected PathfindingRangeCommand(VisualPathfindingRange<TVertex> endPoints)
        {
            pathfindingRange = endPoints;
        }

        public abstract void Execute(IVisualizable obj);

        public abstract bool CanExecute(IVisualizable obj);
    }
}