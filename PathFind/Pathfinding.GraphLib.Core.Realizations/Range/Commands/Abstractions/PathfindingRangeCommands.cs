using Pathfinding.GraphLib.Core.Interface;
using Shared.Collections;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using Shared.Primitives.Attributes;
using Shared.Primitives.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions
{
    public abstract class PathfindingRangeCommands<TVertex> : IExecutable<TVertex>
        where TVertex : IVertex
    {
        private readonly Lazy<ReadOnlyList<PathfindingRangeCommand<TVertex>>> executables;

        protected readonly PathfindingRange<TVertex> pathfindingRange;

        protected ReadOnlyList<PathfindingRangeCommand<TVertex>> Executables => executables.Value;

        protected PathfindingRangeCommands(PathfindingRange<TVertex> pathfindingRange)
        {
            this.pathfindingRange = pathfindingRange;
            executables = new Lazy<ReadOnlyList<PathfindingRangeCommand<TVertex>>>(GetCommands()
                .OrderByOrderAttribute<PathfindingRangeCommand<TVertex>, OrderAttribute>()
                .ToReadOnly);
        }

        public void Execute(TVertex vertex)
        {
            Executables.Execute(vertex);
        }

        protected abstract IEnumerable<PathfindingRangeCommand<TVertex>> GetCommands();
    }
}