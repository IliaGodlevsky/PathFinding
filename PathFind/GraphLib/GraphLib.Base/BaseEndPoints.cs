using Common.Extensions;
using GraphLib.Base.BaseEndPointsConditions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Base
{
    public abstract class BaseEndPoints : IIntermediateEndPoints
    {
        public IVertex Source { get; internal set; }

        public IVertex Target { get; internal set; }

        public IReadOnlyCollection<IVertex> IntermediateVertices => intermediates;

        public bool HasIsolators => !HasEndPointsSet || HasIsolatedIntermediates;

        public void SubscribeToEvents(IGraph graph) => graph.ForEach(SubscribeVertex);
        public void UnsubscribeFromEvents(IGraph graph) => graph.ForEach(UnsubscribeVertex);

        public void Reset() => endPointsConditions.Reset();
        public bool IsEndPoint(IVertex vertex) => vertex.IsOneOf(this);
        public bool CanBeEndPoint(IVertex vertex) => !IsEndPoint(vertex) && !vertex.IsIsolated();

        internal bool HasIsolatedIntermediates => intermediates.Any(vertex => vertex.IsIsolated());
        internal bool HasEndPointsSet => !Source.IsIsolated() && !Target.IsIsolated();
        internal bool IsIntermediate(IVertex vertex) => intermediates.Contains(vertex);

        internal readonly List<IVertex> intermediates;

        protected BaseEndPoints()
        {
            intermediates = new List<IVertex>();
            endPointsConditions = new EndPointsConditions(this);
            Reset();
        }

        protected virtual void SetEndPoints(object sender, EventArgs e)
        {
            endPointsConditions.ExecuteTheFirstTrue(sender as IVertex);
        }

        protected abstract void SubscribeVertex(IVertex vertex);
        protected abstract void UnsubscribeVertex(IVertex vertex);

        private readonly EndPointsConditions endPointsConditions;
    }
}