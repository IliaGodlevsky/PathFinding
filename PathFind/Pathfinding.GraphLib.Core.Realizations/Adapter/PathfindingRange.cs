using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Realizations.VisualizationCommands;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter
{
    public class PathfindingRange<TVertex> : IPathfindingRange
        where TVertex : IVertex
    {
        private readonly IntermediatesToRemoveCommands<TVertex> intermediateVerticesToRemoveCommands;
        private readonly IncludeInRangeCommands<TVertex> includeCommands;

        IVertex IPathfindingRange.Source => Source;

        IVertex IPathfindingRange.Target => Target;

        public virtual TVertex Source { get; internal protected set; }

        public virtual TVertex Target { get; internal protected set; }

        internal protected virtual IList<TVertex> Intermediates { get; }

        internal protected virtual IList<TVertex> ToReplaceIntermediates { get; }

        public PathfindingRange()
        {
            Intermediates = new List<TVertex>();
            ToReplaceIntermediates = new List<TVertex>();
            includeCommands = new IncludeInRangeCommands<TVertex>(this);
            intermediateVerticesToRemoveCommands = new IntermediatesToRemoveCommands<TVertex>(this);
        }

        public void Undo()
        {
            includeCommands.Undo();
            intermediateVerticesToRemoveCommands.Undo();
        }

        public virtual void IncludeInPathfindingRange(TVertex vertex)
        {
            includeCommands.Execute(vertex);
        }

        protected virtual void MarkIntermediateVertexToReplace(TVertex vertex)
        {
            intermediateVerticesToRemoveCommands.Execute(vertex);
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
