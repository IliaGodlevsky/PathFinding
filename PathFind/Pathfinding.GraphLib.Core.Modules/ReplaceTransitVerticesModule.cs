using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules.Commands;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Modules
{
    public sealed class ReplaceTransitVerticesModule<TVertex> : IUndo
        where TVertex : IVertex
    {
        private readonly IEnumerable<IReplaceTransitCommand<TVertex>> markCommands;
        private readonly IEnumerable<IUndo> undoCommands;
        private readonly IPathfindingRangeCommand<TVertex> replaceCommand;
        private readonly IPathfindingRange<TVertex> range;

        internal IList<TVertex> TransitVerticesToReplace { get; } = new List<TVertex>();

        public ReplaceTransitVerticesModule(IPathfindingRange<TVertex> range)
        {
            this.range = range;
            markCommands = GetMarkCommands().ToReadOnly();
            undoCommands = markCommands.OfType<IUndo>().ToReadOnly();
            replaceCommand = new ReplaceTransitVertex<TVertex>(this);
        }

        public void Undo()
        {
            undoCommands.Undo();
        }

        public void ReplaceTransitWith(TVertex vertex)
        {
            if (replaceCommand.CanExecute(range, vertex))
            {
                replaceCommand.Execute(range, vertex);
            }
        }

        public void MarkTransitVertex(TVertex vertex)
        {
            markCommands.FirstOrDefault(command => command.CanExecute(range, vertex))?.Execute(vertex);
        }

        private IEnumerable<IReplaceTransitCommand<TVertex>> GetMarkCommands()
        {
            // Order sensitive
            yield return new RemoveMarkToReplaceTransitVertex<TVertex>(this);
            yield return new MarkToReplaceTransitVertex<TVertex>(this);
        }
    }
}
