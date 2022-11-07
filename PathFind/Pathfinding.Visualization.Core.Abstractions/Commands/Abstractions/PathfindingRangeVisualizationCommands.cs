using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using System.Collections.Generic;

namespace Pathfinding.Visualization.Core.Abstractions.Commands.Abstractions
{
    internal abstract class PathfindingRangeVisualizationCommands<TVertex> : IExecutable<IVisualizable>, IUndo
        where TVertex : IVertex, IVisualizable
    {
        protected IEnumerable<IVisualizationCommand> ExecuteCommands { get; }

        protected IEnumerable<IUndo> UndoCommands { get; }

        protected PathfindingRangeVisualizationCommands(VisualPathfindingRange<TVertex> endPoints)
        {
            ExecuteCommands = GetCommands(endPoints).OrderByOrderAttribute().ToReadOnly();
            UndoCommands = GetUndoCommands(endPoints).ToReadOnly();
        }

        public void Execute(IVisualizable vertex)
        {
            ExecuteCommands.ExecuteFirst(vertex);
        }

        public void Undo()
        {
            UndoCommands.Undo();
        }

        protected abstract IEnumerable<IVisualizationCommand> GetCommands(VisualPathfindingRange<TVertex> endPoints);

        protected abstract IEnumerable<IUndo> GetUndoCommands(VisualPathfindingRange<TVertex> endPoints);
    }
}