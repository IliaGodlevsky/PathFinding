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
    internal sealed class SetPathfindingRangeCommands<TVertex> : PathfindingRangeVisualizationCommands<TVertex>, IUndo
        where TVertex : IVertex, IVisualizable
    {
        private ReadOnlyList<IUndo> UndoCommands { get; }

        public SetPathfindingRangeCommands(PathfindingRangeAdapter<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
            UndoCommands = ExecuteCommands.OfType<IUndo>().ToReadOnly();
        }

        protected override IEnumerable<IVisualizationCommand<TVertex>> GetCommands()
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