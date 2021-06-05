using Common.Extensions;
using Conditional;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using NullObject.Extensions;
using System;

namespace GraphLib.Base
{
    public abstract class BaseEndPoints : IEndPoints
    {
        protected BaseEndPoints()
        {
            Reset();
            If = new If<IVertex>(v => Source.IsEqual(v), UnsetSource)
                  .ElseIf(v => Target.IsEqual(v), UnsetTarget)
                  .ElseIf(CanSetSource, SetSource)
                  .ElseIf(v => Source.IsIsolated(), ReplaceSource)
                  .ElseIf(CanSetTarget, SetTarget)
                  .ElseIf(v => Target.IsIsolated(), ReplaceTarget);
        }

        public bool HasEndPointsSet => !Source.IsIsolated() && !Target.IsIsolated();

        public IVertex Source { get; private set; }

        public IVertex Target { get; private set; }

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
            Source = new NullVertex();
            Target = new NullVertex();
        }

        public bool IsEndPoint(IVertex vertex)
        {
            return vertex.IsEqual(Source) || vertex.IsEqual(Target);
        }

        public bool CanBeEndPoint(IVertex vertex)
        {
            return !IsEndPoint(vertex) && !vertex.IsIsolated();
        }

        protected bool CanSetSource(IVertex vertex)
        {
            return Source.IsNullObject() && CanBeEndPoint(vertex);
        }

        protected bool CanSetTarget(IVertex vertex)
        {
            return !Source.IsNullObject() && Target.IsNullObject() && CanBeEndPoint(vertex);
        }

        protected virtual void SetEndPoints(object sender, EventArgs e)
        {
            bool IsNotIsolated(IVertex vertex) => vertex?.IsIsolated() == false;
            If.WalkThroughConditions(parametre: sender as IVertex, IsNotIsolated);
        }

        protected virtual void SetSource(IVertex vertex)
        {
            Source = vertex;
            (vertex as IMarkable)?.MarkAsStart();
        }

        protected virtual void SetTarget(IVertex vertex)
        {
            Target = vertex;
            (vertex as IMarkable)?.MarkAsEnd();

        }

        protected virtual void UnsetSource(IVertex vertex)
        {
            (vertex as IMarkable)?.MarkAsRegular();
            Source = new NullVertex();
        }

        protected virtual void UnsetTarget(IVertex vertex)
        {
            (vertex as IMarkable)?.MarkAsRegular();
            Target = new NullVertex();
        }

        protected virtual void ReplaceSource(IVertex vertex)
        {
            UnsetSource(Source);
            SetSource(vertex);
        }

        protected virtual void ReplaceTarget(IVertex vertex)
        {
            UnsetTarget(Target);
            SetTarget(vertex);
        }

        protected abstract void SubscribeVertex(IVertex vertex);
        protected abstract void UnsubscribeVertex(IVertex vertex);

        private If<IVertex> If { get; }
    }
}
