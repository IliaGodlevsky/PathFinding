using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;

namespace Pathfinding.AlgorithmLib.Visualization.Commands.Abstractions
{
    internal abstract class PathfindingRangeUndoCommand<TVertex> : IUndo
        where TVertex : IVertex, IVisualizable
    {
        protected readonly VisualPathfindingRange<TVertex> pathfindingRange;

        protected TVertex Source => pathfindingRange.Source;

        protected TVertex Target => pathfindingRange.Target;

        protected PathfindingRangeUndoCommand(VisualPathfindingRange<TVertex> pathfindingRange)
        {
            this.pathfindingRange = pathfindingRange;
        }

        public abstract void Undo();
    }
}