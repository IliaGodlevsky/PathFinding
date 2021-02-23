using Common;
using Common.Extensions;
using GraphLib.Common.NullObjects;
using GraphLib.Extensions;
using GraphLib.Interface;
using System;
using System.Collections.Generic;

namespace GraphLib.Base
{
    public abstract class BaseEndPoints : IEndPoints
    {
        public BaseEndPoints()
        {
            Reset();
            commands = new Command<IVertex>[]
            {
                new Command<IVertex>(vertex => Start.IsEqual(vertex), UnsetStartVertex),
                new Command<IVertex>(vertex => Start.IsDefault(),     SetStartVertex),
                new Command<IVertex>(vertex => Start.IsIsolated(),    ReplaceStartVertex),
                new Command<IVertex>(vertex => End.IsEqual(vertex),   UnsetEndVertex),
                new Command<IVertex>(vertex => CanSetEndVertex,       SetEndVertex),
                new Command<IVertex>(vertex => End.IsIsolated(),      ReplaceEndVertex)
            };
        }

        public BaseEndPoints(IVertex start, IVertex end) : this()
        {
            Start = start;
            End = end;
        }

        public bool HasEndPointsSet => !Start.IsObstacle && !End.IsObstacle;

        public IVertex Start { get; private set; }

        public IVertex End { get; private set; }

        protected bool CanSetEndVertex 
            => !Start.IsDefault() && End.IsDefault();

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
            if (sender is IVertex vertex && !vertex.IsIsolated())
            {
                Command<IVertex>.ExecuteFirstExecutable(commands, vertex);
            }
        }

        protected virtual void SetStartVertex(IVertex vertex)
        {
            if (CanBeEndPoint(vertex))
            {
                Start = vertex;
                (vertex as IMarkableVertex)?.MarkAsStart();
            }
        }

        protected virtual void SetEndVertex(IVertex vertex)
        {
            if (CanBeEndPoint(vertex))
            {
                End = vertex;
                (vertex as IMarkableVertex)?.MarkAsEnd();
            }
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

        private readonly IEnumerable<Command<IVertex>> commands;
    }
}
