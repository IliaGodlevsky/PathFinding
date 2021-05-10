using Common.Extensions;
using Conditional;
using GraphLib.Common.NullObjects;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;

namespace GraphLib.Base
{
    public abstract class BaseEndPoints : IEndPoints
    {
        protected BaseEndPoints()
        {
            Reset();
            If = new If<IVertex>(v => Start.IsEqual(v), UnsetStartVertex)
                  .ElseIf(v => End.IsEqual(v), UnsetEndVertex)
                  .ElseIf(CanSetStartVertex, SetStartVertex)
                  .ElseIf(v => Start.IsIsolated(), ReplaceStartVertex)
                  .ElseIf(CanSetEndVertex, SetEndVertex)
                  .ElseIf(v => End.IsIsolated(), ReplaceEndVertex);
        }

        public bool HasEndPointsSet => !Start.IsIsolated() && !End.IsIsolated();

        public IVertex Start { get; private set; }

        public IVertex End { get; private set; }

        public void SubscribeToEvents(IGraph graph)
        {
            graph.ForEach(SubscribeVertex);
        }

        public void UnsubscribeFromEvents(IGraph graph)
        {
            graph.ForEach(UnsubscribeVertex);
        }

        public void Reset()
        {
            Start = new NullVertex();
            End = new NullVertex();
        }

        public bool IsEndPoint(IVertex vertex)
        {
            return vertex.IsEqual(Start) || vertex.IsEqual(End);
        }

        public bool CanBeEndPoint(IVertex vertex)
        {
            return !IsEndPoint(vertex) && !vertex.IsIsolated();
        }

        protected bool CanSetStartVertex(IVertex vertex)
        {
            return Start.IsNullObject() && CanBeEndPoint(vertex);
        }

        protected bool CanSetEndVertex(IVertex vertex)
        {
            return !Start.IsNullObject() && End.IsNullObject() && CanBeEndPoint(vertex);
        }

        protected virtual void SetEndPoints(object sender, EventArgs e)
        {
            bool IsNotIsolated(IVertex vertex) => vertex?.IsIsolated() == false;
            If.Walk(parametre: sender as IVertex, walkCondition: IsNotIsolated);
        }

        protected virtual void SetStartVertex(IVertex vertex)
        {
            Start = vertex;
            (vertex as IMarkable)?.MarkAsStart();
        }

        protected virtual void SetEndVertex(IVertex vertex)
        {
            End = vertex;
            (vertex as IMarkable)?.MarkAsEnd();

        }

        protected virtual void UnsetStartVertex(IVertex vertex)
        {
            (vertex as IMarkable)?.MarkAsRegular();
            Start = new NullVertex();
        }

        protected virtual void UnsetEndVertex(IVertex vertex)
        {
            (vertex as IMarkable)?.MarkAsRegular();
            End = new NullVertex();
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

        private If<IVertex> If { get; }
    }
}
