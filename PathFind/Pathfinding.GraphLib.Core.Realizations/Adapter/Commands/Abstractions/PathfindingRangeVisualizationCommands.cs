using Pathfinding.GraphLib.Core.Interface;
using Shared.Collections;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using Shared.Primitives.Attributes;
using Shared.Primitives.Extensions;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions
{
    public abstract class PathfindingRangeCommands<TVertex> : IExecutable<TVertex>
        where TVertex : IVertex
    {
        protected readonly PathfindingRange<TVertex> adapter;

        protected ReadOnlyList<PathfindingRangeCommand<TVertex>> ExecuteCommands { get; }

        protected PathfindingRangeCommands(PathfindingRange<TVertex> adapter)
        {
            this.adapter = adapter;
            ExecuteCommands = GetCommands()
                .OrderByOrderAttribute<PathfindingRangeCommand<TVertex>, OrderAttribute>()
                .ToReadOnly();
        }

        public void Execute(TVertex vertex)
        {
            ExecuteCommands.Execute(vertex);
        }

        protected abstract IEnumerable<PathfindingRangeCommand<TVertex>> GetCommands();
    }
}