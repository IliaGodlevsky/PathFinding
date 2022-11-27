using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.CommandCollections;
using Shared.Executable;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Range
{
    public class PathfindingRange<TVertex> : IPathfindingRange, IUndo
        where TVertex : IVertex
    {
        private readonly IncludeInRangeCommands<TVertex> includeCommands;

        IVertex IPathfindingRange.Source => Source;

        IVertex IPathfindingRange.Target => Target;

        public virtual TVertex Source { get; internal protected set; }

        public virtual TVertex Target { get; internal protected set; }

        internal protected virtual IList<TVertex> Intermediates { get; }

        public PathfindingRange()
        {
            Intermediates = new List<TVertex>();
            includeCommands = new IncludeInRangeCommands<TVertex>(this);
        }

        public void Undo()
        {
            includeCommands.Undo();
        }

        public virtual void IncludeInPathfindingRange(TVertex vertex)
        {
            includeCommands.Execute(vertex);
        }

        public IEnumerator<IVertex> GetEnumerator()
        {
            return Intermediates.Append(Target)
                .Prepend(Source)
                .OfType<IVertex>()
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}