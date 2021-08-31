using GraphLib.Base.EndPointsCondition;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphLib.Base
{
    public abstract class BaseEndPoints : IIntermediateEndPoints
    {
        public IVertex Source { get; internal set; }
        public IVertex Target { get; internal set; }
        public IReadOnlyCollection<IVertex> IntermediateVertices => intermediates;

        public void SubscribeToEvents(IGraph graph) => graph.ForEach(SubscribeVertex);
        public void UnsubscribeFromEvents(IGraph graph) => graph.ForEach(UnsubscribeVertex);
        public void Reset() => endPointsConditions.Reset();
        public bool IsEndPoint(IVertex vertex) => this.GetVertices().Contains(vertex);

        internal readonly Collection<IVertex> intermediates;
        internal readonly Queue<IVertex> markedToReplaceIntermediates;

        protected BaseEndPoints()
        {
            intermediates = new Collection<IVertex>();
            markedToReplaceIntermediates = new Queue<IVertex>();
            endPointsConditions = new EndPointsConditions(this);
            Reset();
        }

        protected virtual void SetEndPoints(object sender, EventArgs e)
        {
            endPointsConditions.ExecuteTheFirstTrue(sender as IVertex);
        }

        protected virtual void MarkIntermediateToReplace(object sender, EventArgs e)
        {
            if (sender is IVertex vertex 
                && !vertex.IsOneOf(Source, Target)
                && intermediates.Contains(vertex)
                && !vertex.IsIsolated())
            {
                markedToReplaceIntermediates.Enqueue(vertex);
            }
        }

        protected abstract void SubscribeVertex(IVertex vertex);
        protected abstract void UnsubscribeVertex(IVertex vertex);

        private readonly EndPointsConditions endPointsConditions;
    }
}