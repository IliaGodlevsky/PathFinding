using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Modules.Commands
{
    public sealed class IncludeTransitVertex<TVertex> : IPathfindingRangeCommand<TVertex>, IUndoCommand<TVertex>
        where TVertex : IVertex
    {
        private readonly IPathfindingRangeCommand<TVertex> undoCommand;

        public IncludeTransitVertex()
        {
            undoCommand = new ExcludeTransitVertex<TVertex>();
        }

        public void Execute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            range.Transit.Add(vertex);
        }

        public bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return range.HasSourceAndTargetSet()
                && range.CanBeInRange(vertex);
        }

        public void Undo(IPathfindingRange<TVertex> range)
        {
            foreach (var vertex in range.Transit.ToArray())
            {
                undoCommand.Execute(range, vertex);
            }
        }
    }
}