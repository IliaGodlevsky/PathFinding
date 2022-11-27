using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.PathfindingRangeCommands;
using Shared.Collections;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.CommandCollections
{
    internal sealed class IncludeInRangeCommands<TVertex> : PathfindingRangeCommands<TVertex>, IUndo
        where TVertex : IVertex
    {
        private ReadOnlyList<IUndo> UndoCommands { get; }

        public IncludeInRangeCommands(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
            UndoCommands = Executables.OfType<IUndo>().ToReadOnly();
        }

        public void Undo()
        {
            UndoCommands.Undo();
        }

        protected override IEnumerable<PathfindingRangeCommand<TVertex>> GetCommands()
        {
            yield return new ExcludeIntermediateVertex<TVertex>(pathfindingRange);
            yield return new ExcludeTargetVertex<TVertex>(pathfindingRange);
            yield return new ExcludeSourceVertex<TVertex>(pathfindingRange);
            yield return new IncludeSourceVertex<TVertex>(pathfindingRange);
            yield return new IncludeTargetVertex<TVertex>(pathfindingRange);
            yield return new IncludeIntermediateVertex<TVertex>(pathfindingRange);
            yield return new ReplaceIntermediateIsolatedVertex<TVertex>(pathfindingRange);
            yield return new ReplaceIsolatedSourceVertex<TVertex>(pathfindingRange);
            yield return new ReplaceIsolatedTargetVertex<TVertex>(pathfindingRange);
        }
    }
}