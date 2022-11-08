using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Pathfinding.GraphLib.Visualization.Commands.Abstractions
{
    internal abstract class PathfindingRangeCommand<TVertex> : IVisualizationCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected readonly VisualPathfindingRange<TVertex> pathfindingRange;

        protected IList<TVertex> IntermediateVertices => pathfindingRange.IntermediateVertices;

        protected IList<TVertex> MarkedToRemoveIntermediates => pathfindingRange.MarkedToRemoveIntermediateVertices;

        protected TVertex Source => pathfindingRange.Source;

        protected TVertex Target => pathfindingRange.Target;

        protected PathfindingRangeCommand(VisualPathfindingRange<TVertex> endPoints)
        {
            pathfindingRange = endPoints;
        }

        public abstract void Execute(TVertex obj);

        public abstract bool CanExecute(TVertex obj);
    }
}