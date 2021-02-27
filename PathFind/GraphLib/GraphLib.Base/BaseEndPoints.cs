using Common.Extensions;
using Conditional;
using GraphLib.Common.NullObjects;
using GraphLib.Extensions;
using GraphLib.Interface;
using System;

namespace GraphLib.Base
{
    public abstract class BaseEndPoints : IEndPoints
    {
        public BaseEndPoints()
        {
            Reset();
            If = new If(o => Start.IsEqual((IVertex)o),       o => UnsetStartVertex((IVertex)o))
                  .ElseIf(o => End.IsEqual((IVertex)o),       o => UnsetEndVertex((IVertex)o))
                  .ElseIf(o => CanSetStartVertex((IVertex)o), o => SetStartVertex((IVertex)o))
                  .ElseIf(o => Start.IsIsolated(),            o => ReplaceStartVertex((IVertex)o))
                  .ElseIf(o => CanSetEndVertex((IVertex)o),   o => SetEndVertex((IVertex)o))
                  .ElseIf(o => End.IsIsolated(),              o => ReplaceEndVertex((IVertex)o));
        }

        public BaseEndPoints(IVertex start, IVertex end) : this()
        {
            Start = start;
            End = end;
        }

        public bool HasEndPointsSet => !Start.IsIsolated() && !End.IsIsolated();

        public IVertex Start { get; private set; }

        public IVertex End { get; private set; }

        protected bool CanSetStartVertex(IVertex vertex)
            => Start.IsDefault() && CanBeEndPoint(vertex);
        protected bool CanSetEndVertex(IVertex vertex)
            => !Start.IsDefault() && End.IsDefault() && CanBeEndPoint(vertex);

        public void SubscribeToEvents(IGraph graph)
        {
            graph.Vertices.ForEach(SubscribeVertex);
        }

        public void UnsubscribeFromEvents(IGraph graph)
        {
            graph.Vertices.ForEach(UnsubscribeVertex);
        }

        public void Reset()
        {
            Start = new DefaultVertex();
            End = new DefaultVertex();
        }

        public bool IsEndPoint(IVertex vertex)
        {
            return vertex.IsEqual(Start) || vertex.IsEqual(End);
        }

        public bool CanBeEndPoint(IVertex vertex)
        {
            return !IsEndPoint(vertex) && vertex.IsValidToBeEndPoint();
        }

        protected virtual void SetEndPoints(object sender, EventArgs e)
        {
            If.Walk(sender as IVertex, 
                obj => (obj as IVertex)?.IsIsolated() == false);
        }

        protected virtual void SetStartVertex(IVertex vertex)
        {
            Start = vertex;
            (vertex as IMarkableVertex)?.MarkAsStart();
        }

        protected virtual void SetEndVertex(IVertex vertex)
        {
            End = vertex;
            (vertex as IMarkableVertex)?.MarkAsEnd();

        }

        protected virtual void UnsetStartVertex(IVertex vertex)
        {
            (vertex as IMarkableVertex)?.MarkAsSimpleVertex();
            Start = new DefaultVertex();
        }

        protected virtual void UnsetEndVertex(IVertex vertex)
        {
            (vertex as IMarkableVertex)?.MarkAsSimpleVertex();
            End = new DefaultVertex();
        }

        protected virtual void ReplaceStartVertex(IVertex vertex)
        {
            UnsetStartVertex(Start);
            SetStartVertex(vertex);
        }

        protected virtual void ReplaceEndVertex(IVertex vertex)
        {
            UnsetEndVertex(End);
            SetEndVertex(vertex);
        }

        protected abstract void SubscribeVertex(IVertex vertex);
        protected abstract void UnsubscribeVertex(IVertex vertex);

        private If If { get; }
    }
}
