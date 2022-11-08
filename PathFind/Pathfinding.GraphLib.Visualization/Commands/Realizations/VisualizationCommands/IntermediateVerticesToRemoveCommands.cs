using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Visualization.Commands.Abstractions;
using Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Collections;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.VisualizationCommands
{
    internal sealed class IntermediateVerticesToRemoveCommands<TVertex> : PathfindingRangeVisualizationCommands<TVertex>, IUndo
        where TVertex : IVertex, IVisualizable
    {
        private ReadOnlyList<IUndo> UndoCommands { get; }

        public IntermediateVerticesToRemoveCommands(VisualPathfindingRange<TVertex> endPoints)
            : base(endPoints)
        {
            UndoCommands = ExecuteCommands.OfType<IUndo>().ToReadOnly();
        }

        public void Undo()
        {
            UndoCommands.Undo();
        }

        protected override IEnumerable<IVisualizationCommand<TVertex>> GetCommands()
        {
            yield return new RemoveMarkToReplaceIntermediateVertexCommand<TVertex>(pathfindingRange);
            yield return new MarkToReplaceIntermediateVertexCommand<TVertex>(pathfindingRange);
        }
    }
}