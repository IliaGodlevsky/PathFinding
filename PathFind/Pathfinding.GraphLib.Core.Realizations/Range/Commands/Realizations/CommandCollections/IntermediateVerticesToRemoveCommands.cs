using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.PathfindingRangeCommands;
using Shared.Collections;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using Shared.Primitives.Attributes;
using Shared.Primitives.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.CommandCollections
{
    internal sealed class IntermediatesToRemoveCommands<TVertex> : PathfindingRangeCommands<TVertex>, IUndo
        where TVertex : IVertex
    {
        private readonly ReplaceIntermediateVerticesModule<TVertex> module;

        private ReadOnlyList<IUndo> UndoCommands { get; }

        public IntermediatesToRemoveCommands(PathfindingRange<TVertex> pathfindingRange, 
            ReplaceIntermediateVerticesModule<TVertex> module)
            : base(pathfindingRange)
        {
            this.module = module;
            UndoCommands = Executables.OfType<IUndo>().ToReadOnly();
        }

        public void Undo()
        {
            UndoCommands.Undo();
        }

        protected override IEnumerable<PathfindingRangeCommand<TVertex>> GetCommands()
        {
            yield return new RemoveMarkToReplaceIntermediateVertexCommand<TVertex>(pathfindingRange, module);
            yield return new ReplaceIntermediateVertex<TVertex>(pathfindingRange, module);
            yield return new MarkToReplaceIntermediateVertexCommand<TVertex>(pathfindingRange, module);
        }
    }
}