using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Base
{
    public abstract class BaseEndPoints : IIntermediateEndPoints
    {
        protected BaseEndPoints()
        {
            intermediate = new List<IVertex>();
            Reset();
            conditions = new Dictionary<Predicate<IVertex>, Action<IVertex>>
            {
                { v => Source.IsEqual(v), UnsetSource},
                { v => Target.IsEqual(v), UnsetTarget},
                { CanUnsetIntermediate, UnsetIntermediate },
                { CanReplaceIntermediate, ReplaceIntermediate },
                { CanSetSource, SetSource },
                { v => Source.IsIsolated(), ReplaceSource},
                { CanSetTarget , SetTarget},
                { v => Target.IsIsolated(), ReplaceTarget},
                { v => HasEndPointsSet, SetIntermediate}                
            };
        }

        public bool HasIsolators => !HasEndPointsSet || HasIsolatedIntermediates;

        private bool HasEndPointsSet => !Source.IsIsolated() && !Target.IsIsolated();

        public IVertex Source { get; private set; }

        public IVertex Target { get; private set; }

        public IReadOnlyCollection<IVertex> IntermediateVertices => intermediate;

        public void SubscribeToEvents(IGraph graph) => graph.ForEach(SubscribeVertex);

        public void UnsubscribeFromEvents(IGraph graph) => graph.ForEach(UnsubscribeVertex);

        public void Reset()
        {
            UnsetSource(Source);
            UnsetTarget(Target);
            IntermediateVertices.ForEach(UnsetIntermediate);
        }

        public bool IsEndPoint(IVertex vertex) => vertex.IsEqual(Source) || vertex.IsEqual(Target) || IsIntermediate(vertex);

        public bool CanBeEndPoint(IVertex vertex) => !IsEndPoint(vertex) && !vertex.IsIsolated();

        protected bool CanUnsetIntermediate(IVertex vertex) => HasEndPointsSet && IsIntermediate(vertex);

        protected bool CanReplaceIntermediate(IVertex vertex) 
            => HasEndPointsSet && !IsIntermediate(vertex) && HasIsolatedIntermediates;

        protected bool CanSetSource(IVertex vertex) => Source.IsNull() && CanBeEndPoint(vertex);

        protected bool CanSetTarget(IVertex vertex) => !Source.IsNull() && Target.IsNull() && CanBeEndPoint(vertex);

        protected virtual void SetEndPoints(object sender, EventArgs e)
        {
            if (sender is IVertex vertex && !vertex.IsIsolated())
            {
                conditions.FirstOrDefault(item => item.Key?.Invoke(vertex) == true).Value?.Invoke(vertex);
            }
        }

        protected virtual void SetSource(IVertex vertex)
        {
            Source = vertex;
            (vertex as IMarkable)?.MarkAsSource();
        }

        protected virtual void SetTarget(IVertex vertex)
        {
            Target = vertex;
            (vertex as IMarkable)?.MarkAsTarget();
        }

        protected virtual void SetIntermediate(IVertex vertex)
        {
            intermediate.Add(vertex);
            (vertex as IMarkable)?.MarkAsIntermediate();
        }

        protected virtual void UnsetSource(IVertex vertex)
        {
            (vertex as IMarkable)?.MarkAsRegular();
            Source = new NullVertex();
        }

        protected virtual void UnsetIntermediate(IVertex vertex)
        {
            intermediate.Remove(vertex);
            (vertex as IMarkable)?.MarkAsRegular();
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

        protected virtual void ReplaceIntermediate(IVertex vertex)
        {
            var isolated = intermediate.FirstOrDefault(v => v.IsIsolated());
            if(!isolated.IsNull())
            {
                int isolatedIndex = intermediate.IndexOf(isolated);
                UnsetIntermediate(isolated);
                intermediate.Insert(isolatedIndex, vertex);
                (vertex as IMarkable)?.MarkAsIntermediate();
            }
        }

        private bool IsIntermediate(IVertex vertex) => intermediate.Contains(vertex);

        private bool HasIsolatedIntermediates => intermediate.Any(vertex => vertex.IsIsolated());

        protected abstract void SubscribeVertex(IVertex vertex);
        protected abstract void UnsubscribeVertex(IVertex vertex);

        private readonly List<IVertex> intermediate;
        private readonly Dictionary<Predicate<IVertex>, Action<IVertex>> conditions;
    }
}
