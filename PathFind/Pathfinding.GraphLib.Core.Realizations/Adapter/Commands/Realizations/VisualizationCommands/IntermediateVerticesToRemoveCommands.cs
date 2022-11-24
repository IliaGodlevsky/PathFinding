using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Realizations.PathfindingRangeCommands;
using Shared.Collections;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Realizations.VisualizationCommands
{
    internal sealed class IntermediatesToRemoveCommands<TVertex> : PathfindingRangeCommands<TVertex>, IUndo
        where TVertex : IVertex
    {
        private ReadOnlyList<IUndo> UndoCommands { get; }

        public IntermediatesToRemoveCommands(PathfindingRange<TVertex> adapter)
            : base(adapter)
        {
            UndoCommands = ExecuteCommands.OfType<IUndo>().ToReadOnly();
        }

        public void Undo()
        {
            UndoCommands.Undo();
        }

        protected override IEnumerable<PathfindingRangeCommand<TVertex>> GetCommands()
        {
            yield return new RemoveMarkToReplaceIntermediateVertexCommand<TVertex>(adapter);
            yield return new MarkToReplaceIntermediateVertexCommand<TVertex>(adapter);
        }
    }
}