using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Visualization.Commands.Abstractions
{
    internal abstract class PathfindingRangeVisualizationCommands<TVertex> : IExecutable<TVertex>, IUndo
        where TVertex : IVertex, IVisualizable
    {
        protected IEnumerable<IVisualizationCommand<TVertex>> ExecuteCommands { get; }

        protected IEnumerable<IUndo> UndoCommands { get; }

        protected PathfindingRangeVisualizationCommands(VisualPathfindingRange<TVertex> endPoints)
        {
            ExecuteCommands = GetCommands(endPoints).OrderByOrderAttribute().ToReadOnly();
            UndoCommands = GetUndoCommands(endPoints).ToReadOnly();
        }

        public void Execute(TVertex vertex)
        {
            ExecuteCommands.ExecuteFirst(vertex);
        }

        public void Undo()
        {
            UndoCommands.Undo();
        }

        protected abstract IEnumerable<IVisualizationCommand<TVertex>> GetCommands(VisualPathfindingRange<TVertex> endPoints);

        protected abstract IEnumerable<IUndo> GetUndoCommands(VisualPathfindingRange<TVertex> endPoints);
    }
}