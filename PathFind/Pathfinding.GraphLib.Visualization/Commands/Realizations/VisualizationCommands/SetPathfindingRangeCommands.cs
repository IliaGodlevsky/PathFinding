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

        public SetPathfindingRangeCommands(VisualPathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
            UndoCommands = ExecuteCommands.OfType<IUndo>().ToReadOnly();
        }

        protected override IEnumerable<IVisualizationCommand<TVertex>> GetCommands()
        {
            yield return new UnsetIntermediateVertexCommand<TVertex>(pathfindingRange);
            yield return new UnsetTargetVertexCommand<TVertex>(pathfindingRange);
            yield return new UnsetSourceVertexCommand<TVertex>(pathfindingRange);
            yield return new SetSourceVertexCommand<TVertex>(pathfindingRange);
            yield return new SetTargetVertexCommand<TVertex>(pathfindingRange);
            yield return new SetIntermediateVertexCommand<TVertex>(pathfindingRange);
            yield return new ReplaceIntermediateVertexCommand<TVertex>(pathfindingRange);
            yield return new ReplaceIntermediateIsolatedVertexCommand<TVertex>(pathfindingRange);
            yield return new ReplaceIsolatedSourceVertexCommand<TVertex>(pathfindingRange);
            yield return new ReplaceIsolatedTargetVertexCommand<TVertex>(pathfindingRange);
        }

        public void Undo()
        {
            UndoCommands.Undo();
        }
    }
}