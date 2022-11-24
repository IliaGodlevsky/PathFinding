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
    internal sealed class IncludeInRangeCommands<TVertex> : PathfindingRangeCommands<TVertex>, IUndo
        where TVertex : IVertex
    {
        private ReadOnlyList<IUndo> UndoCommands { get; }

        public IncludeInRangeCommands(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
            UndoCommands = ExecuteCommands.OfType<IUndo>().ToReadOnly();
        }

        protected override IEnumerable<PathfindingRangeCommand<TVertex>> GetCommands()
        {
            yield return new UnsetIntermediateVertexCommand<TVertex>(adapter);
            yield return new UnsetTargetVertexCommand<TVertex>(adapter);
            yield return new UnsetSourceVertexCommand<TVertex>(adapter);
            yield return new SetSourceVertexCommand<TVertex>(adapter);
            yield return new SetTargetVertexCommand<TVertex>(adapter);
            yield return new SetIntermediateVertexCommand<TVertex>(adapter);
            yield return new ReplaceIntermediateVertexCommand<TVertex>(adapter);
            yield return new ReplaceIntermediateIsolatedVertexCommand<TVertex>(adapter);
            yield return new ReplaceIsolatedSourceVertexCommand<TVertex>(adapter);
            yield return new ReplaceIsolatedTargetVertexCommand<TVertex>(adapter);
        }

        public void Undo()
        {
            UndoCommands.Undo();
        }
    }
}