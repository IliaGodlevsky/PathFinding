using Common.Extensions;
using GraphLib.Common.NullObjects;
using GraphLib.Extensions;
using GraphLib.Interface;
using System;
using System.Linq;

namespace GraphLib.Base.EndPoints
{
    public abstract class BaseEndPoints : IEndPoints
    {
        public BaseEndPoints()
        {
            Reset();
            commands = new Command[]
            {
                new Command(vertex => Start.IsObstacle,      ChangeStartVertex),
                new Command(vertex => Start.IsEqual(vertex), UnsetStartVertex),
                new Command(vertex => Start.IsDefault,       SetStartVertex),
                new Command(vertex => End.IsObstacle,        ChangeEndVertex),
                new Command(vertex => End.IsEqual(vertex),   UnsetEndVertex),
                new Command(vertex => CanSetEndVertex,       SetEndVertex)
            };
        }

        public BaseEndPoints(IVertex start, IVertex end) : this()
        {
            Start = start;
            End = end;
        }

        public IVertex Start { get; private set; }

        public IVertex End { get; private set; }

        public void SubscribeToEvents(IGraph graph)
        {
            graph.Vertices.ForEach(SubscribeVertex);
        }

        public void UnsubscribeFromEvents(IGraph graph)
        {
            graph.Vertices.ForEach(UnsubscribeVertex);
        }

        public bool IsEndPoint(IVertex vertex)
        {
            return vertex.IsEqual(Start) || vertex.IsEqual(End);
        }

        protected virtual void SetEndPoints(object sender, EventArgs e)
        {
            if (sender is IVertex vertex)
            {
                commands
                    .FirstOrDefault(command => command.CanExecute(vertex) == true)
                    ?.Execute(vertex);

            }
        }

        public bool CanBeEndPoint(IVertex vertex)
        {
            return !IsEndPoint(vertex) && vertex.IsValidToBeEndPoint();
        }

        public void Reset()
        {
            Start = new DefaultVertex();
            End = new DefaultVertex();
        }

        public bool HasEndPointsSet => !Start.IsDefault && !End.IsDefault && !Start.IsObstacle && !End.IsObstacle;

        protected abstract void SubscribeVertex(IVertex vertex);
        protected abstract void UnsubscribeVertex(IVertex vertex);

        protected bool CanSetEndVertex => !Start.IsDefault && End.IsDefault;

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
            Start = new DefaultVertex();
            (vertex as IMarkableVertex)?.MarkAsSimpleVertex();
        }

        protected virtual void UnsetEndVertex(IVertex vertex)
        {
            End = new DefaultVertex();
            (vertex as IMarkableVertex)?.MarkAsSimpleVertex();
        }

        protected virtual void ChangeStartVertex(IVertex vertex)
        {
            Start = new DefaultVertex();
            SetStartVertex(vertex);
        }

        protected virtual void ChangeEndVertex(IVertex vertex)
        {
            End = new DefaultVertex();
            SetEndVertex(vertex);
        }

        private readonly Command[] commands;
    }
}
