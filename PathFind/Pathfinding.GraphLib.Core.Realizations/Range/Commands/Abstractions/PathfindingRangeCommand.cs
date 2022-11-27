using Pathfinding.GraphLib.Core.Interface;
using Shared.Executable;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions
{
    public abstract class PathfindingRangeCommand<TVertex> : IExecutable<TVertex>, IExecutionCheck<TVertex>
        where TVertex : IVertex
    {
        protected readonly PathfindingRange<TVertex> pathfindingRange;

        protected IList<TVertex> IntermediateVertices => pathfindingRange.Intermediates;

        protected TVertex Source => pathfindingRange.Source;

        protected TVertex Target => pathfindingRange.Target;

        protected PathfindingRangeCommand(PathfindingRange<TVertex> pathfindingRange)
        {
            this.pathfindingRange = pathfindingRange;
        }

        public abstract void Execute(TVertex obj);

        public abstract bool CanExecute(TVertex obj);
    }
}