using Commands.Extensions;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphLib.Base.EndPoints
{
    public abstract class BasePathfindingRange : IPathfindingRange, IGraphEvents
    {
        private readonly IVerticesCommands markedToReplaceCommands;
        private readonly IVerticesCommands setPathfindingRangeCommands;
        private readonly IVerticesCommands returnColorsCommands;

        public IVertex Source { get; internal set; }

        public IVertex Target { get; internal set; }

        internal protected Collection<IVertex> Intermediates { get; }

        internal protected Collection<IVertex> MarkedToReplace { get; }

        protected BasePathfindingRange()
        {
            Intermediates = new Collection<IVertex>();
            MarkedToReplace = new Collection<IVertex>();
            setPathfindingRangeCommands = new SetPathfindingRangeCommands(this);
            markedToReplaceCommands = new IntermediateToReplaceCommands(this);
            returnColorsCommands = new RestoreColorsCommands(this);
            Reset();
        }

        public void Subscribe(IGraph graph)
        {
            graph.ForEach(SubscribeVertex);
        }

        public void Unsubscribe(IGraph graph)
        {
            graph.ForEach(UnsubscribeVertex);
        }

        public void Reset()
        {
            markedToReplaceCommands.Undo();
            setPathfindingRangeCommands.Undo();
        }

        public void RestoreCurrentColors()
        {
            returnColorsCommands.Execute(this);
        }

        protected virtual void SetPathfindingRange(object sender, EventArgs e)
        {
            setPathfindingRangeCommands.Execute(sender.AsVertex());
        }

        protected virtual void MarkIntermediateToReplace(object sender, EventArgs e)
        {
            markedToReplaceCommands.Execute(sender.AsVertex());
        }

        protected abstract void SubscribeVertex(IVertex vertex);

        protected abstract void UnsubscribeVertex(IVertex vertex);

        public IEnumerator<IVertex> GetEnumerator()
        {
            return Intermediates.Prepend(Source).Append(Target).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}