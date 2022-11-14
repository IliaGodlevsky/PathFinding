using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Collections;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Visualization.Commands.Abstractions
{
    internal abstract class PathfindingRangeVisualizationCommands<TVertex> : IExecutable<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected readonly PathfindingRangeAdapter<TVertex> adapter;

        protected ReadOnlyList<IVisualizationCommand<TVertex>> ExecuteCommands { get; }

        protected PathfindingRangeVisualizationCommands(PathfindingRangeAdapter<TVertex> adapter)
        {
            this.adapter = adapter;
            ExecuteCommands = GetCommands().OrderByOrderAttribute().ToReadOnly();
        }

        public void Execute(TVertex vertex)
        {
            ExecuteCommands.Execute(vertex);
        }

        protected abstract IEnumerable<IVisualizationCommand<TVertex>> GetCommands();
    }
}