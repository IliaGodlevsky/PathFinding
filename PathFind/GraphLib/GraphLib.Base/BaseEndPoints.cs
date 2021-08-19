﻿using Common.Extensions;
using GraphLib.Base.Objects;
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
            conditions = new ConditionCollection()
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
            UnsetSource(Source);
            UnsetTarget(Target);
            IntermediateVertices.ForEach(UnsetIntermediate);
        }

        public bool IsEndPoint(IVertex vertex)
        {
            return vertex.IsOneOf(this);
        }

        public bool CanBeEndPoint(IVertex vertex)
        {
            return !IsEndPoint(vertex) && !vertex.IsIsolated();
        }

        protected bool CanUnsetIntermediate(IVertex vertex)
        {
            return HasEndPointsSet && IsIntermediate(vertex);
        }

        protected bool CanReplaceIntermediate(IVertex vertex)
        {
            return HasEndPointsSet && !IsIntermediate(vertex) && HasIsolatedIntermediates;
        }

        protected bool CanSetSource(IVertex vertex)
        {
            return Source.IsNull() && CanBeEndPoint(vertex);
        }

        protected bool CanSetTarget(IVertex vertex)
        {
            return !Source.IsNull() && Target.IsNull() && CanBeEndPoint(vertex);
        }

        protected virtual void SetEndPoints(object sender, EventArgs e)
        {
            if (sender is IVertex vertex && !vertex.IsIsolated())
            {
                conditions.PerfromFirstExecutable(vertex);
            }
        }

        protected virtual void SetSource(IVertex vertex)
        {
            Source = vertex;
            (vertex as IVisualizable)?.VisualizeAsSource();
        }

        protected virtual void SetTarget(IVertex vertex)
        {
            Target = vertex;
            (vertex as IVisualizable)?.VisualizeAsTarget();
        }

        protected virtual void SetIntermediate(IVertex vertex)
        {
            intermediate.Add(vertex);
            (vertex as IVisualizable)?.VisualizeAsIntermediate();
        }

        protected virtual void UnsetSource(IVertex vertex)
        {
            (vertex as IVisualizable)?.VisualizeAsRegular();
            Source = new NullVertex();
        }

        protected virtual void UnsetIntermediate(IVertex vertex)
        {
            intermediate.Remove(vertex);
            (vertex as IVisualizable)?.VisualizeAsRegular();
        }

        protected virtual void UnsetTarget(IVertex vertex)
        {
            (vertex as IVisualizable)?.VisualizeAsRegular();
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
            if (!isolated.IsNull())
            {
                int isolatedIndex = intermediate.IndexOf(isolated);
                UnsetIntermediate(isolated);
                intermediate.Insert(isolatedIndex, vertex);
                (vertex as IVisualizable)?.VisualizeAsIntermediate();
            }
        }

        private bool IsIntermediate(IVertex vertex)
        {
            return intermediate.Contains(vertex);
        }

        private bool HasIsolatedIntermediates => intermediate.Any(vertex => vertex.IsIsolated());

        protected abstract void SubscribeVertex(IVertex vertex);
        protected abstract void UnsubscribeVertex(IVertex vertex);

        private readonly List<IVertex> intermediate;
        private readonly ConditionCollection conditions;
    }
}
