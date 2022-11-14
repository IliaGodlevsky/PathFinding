using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Visualization.Commands.Abstractions
{
    internal abstract class PathfindingRangeCommand<TVertex> : IVisualizationCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected readonly PathfindingRangeAdapter<TVertex> adapter;

        protected IList<TVertex> IntermediateVertices => adapter.Intermediates;

        protected IList<TVertex> MarkedToRemoveIntermediates => adapter.MarkedToRemoveIntermediateVertices;

        protected TVertex Source => adapter.Source;

        protected TVertex Target => adapter.Target;

        protected PathfindingRangeCommand(PathfindingRangeAdapter<TVertex> adapter)
        {
            this.adapter = adapter;
        }

        public abstract void Execute(TVertex obj);

        public abstract bool CanExecute(TVertex obj);
    }
}