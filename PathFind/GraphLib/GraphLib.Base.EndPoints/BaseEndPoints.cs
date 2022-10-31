using Commands.Extensions;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphLib.Base.EndPoints
{
    public abstract class BaseEndPoints<TVertex> : IEndPoints, IGraphEvents<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly IVerticesCommands<TVertex> markedToReplaceCommands;
        private readonly IVerticesCommands<TVertex> setEndPointsCommands;
        private readonly IVerticesCommands<TVertex> returnColorsCommands;

        IVertex IEndPoints.Source => Source;

        IVertex IEndPoints.Target => Target;

        public TVertex Source { get; internal set; }

        public TVertex Target { get; internal set; }

        internal protected Collection<TVertex> Intermediates { get; }

        internal protected Collection<TVertex> MarkedToReplace { get; }

        protected BaseEndPoints()
        {
            Intermediates = new Collection<TVertex>();
            MarkedToReplace = new Collection<TVertex>();
            setEndPointsCommands = new SetEndPointsCommands<TVertex>(this);
            markedToReplaceCommands = new IntermediateToReplaceCommands<TVertex>(this);
            returnColorsCommands = new RestoreColorsCommands<TVertex>(this);
        }

        public void Subscribe(IGraph<TVertex> graph)
        {
            graph.ForEach(SubscribeVertex);
        }

        public void Unsubscribe(IGraph<TVertex> graph)
        {
            graph.ForEach(UnsubscribeVertex);
        }

        public void Reset()
        {
            markedToReplaceCommands.Undo();
            setEndPointsCommands.Undo();
        }

        public void RestoreCurrentColors()
        {
            returnColorsCommands.Execute(this.OfType<TVertex>());
        }

        protected virtual void SetEndPoints(object sender, EventArgs e)
        {
            setEndPointsCommands.Execute((TVertex)sender);
        }

        protected virtual void MarkIntermediateToReplace(object sender, EventArgs e)
        {
            markedToReplaceCommands.Execute((TVertex)sender);
        }

        protected abstract void SubscribeVertex(TVertex vertex);

        protected abstract void UnsubscribeVertex(TVertex vertex);

        public IEnumerator<IVertex> GetEnumerator()
        {
            return Intermediates
                .OfType<IVertex>()
                .Prepend(Source)
                .Append(Target)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}