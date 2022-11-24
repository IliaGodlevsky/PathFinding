using Pathfinding.GraphLib.Core.Interface;
using Shared.Executable;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions
{
    public abstract class PathfindingRangeCommand<TVertex> : IExecutable<TVertex>, IExecutionCheck<TVertex>
        where TVertex : IVertex
    {
        protected readonly PathfindingRange<TVertex> adapter;

        protected IList<TVertex> IntermediateVertices => adapter.Intermediates;

        protected IList<TVertex> MarkedToRemoveIntermediates => adapter.ToReplaceIntermediates;

        protected TVertex Source => adapter.Source;

        protected TVertex Target => adapter.Target;

        protected PathfindingRangeCommand(PathfindingRange<TVertex> adapter)
        {
            this.adapter = adapter;
        }

        public abstract void Execute(TVertex obj);

        public abstract bool CanExecute(TVertex obj);
    }
}