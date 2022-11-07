using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections.ObjectModel;

namespace Pathfinding.AlgorithmLib.Visualization.Commands.Abstractions
{
    internal abstract class PathfindingRangeCommand<TVertex> : IVisualizationCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected Collection<TVertex> IntermediateVertices => pathfindingRange.IntermediateVertices;

        protected Collection<TVertex> MarkedToRemoveIntermediates => pathfindingRange.MarkedToRemoveIntermediateVertices;

        protected TVertex Source => pathfindingRange.Source;

        protected TVertex Target => pathfindingRange.Target;

        protected readonly VisualPathfindingRange<TVertex> pathfindingRange;

        protected PathfindingRangeCommand(VisualPathfindingRange<TVertex> endPoints)
        {
            pathfindingRange = endPoints;
        }

        public abstract void Execute(TVertex obj);

        public abstract bool CanExecute(TVertex obj);
    }
}