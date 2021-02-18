using GraphLib.Common.NullObjects;
using GraphLib.Extensions;
using GraphLib.Interface;
using System;
using System.Linq;

namespace GraphLib.Base
{
    public abstract class BaseEndPoints : IEndPoints
    {
        public BaseEndPoints()
        {
            Start = new DefaultVertex();
            End = new DefaultVertex();
        }

        public BaseEndPoints(IVertex start, IVertex end)
        {
            Start = start;
            End = end;
        }

        public IVertex Start { get; private set; }

        public IVertex End { get; private set; }

        public void SubscribeToEvents(IGraph graph)
        {
            graph.Vertices.AsParallel().ForAll(SubscribeVertex);
        }

        public void UnsubscribeFromEvents(IGraph graph)
        {
            graph.Vertices.AsParallel().ForAll(UnsubscribeVertex);
        }

        public bool IsEndPoint(IVertex vertex)
        {
            return vertex.IsEqual(Start) || vertex.IsEqual(End);
        }

        public virtual void SetEndPoints(object sender, EventArgs e)
        {
            if (sender is IVertex vertex)
            {
                if (vertex.IsEqual(Start))
                {
                    UnsetStartVertex(vertex);
                }
                else if (vertex.IsEqual(End))
                {
                    UnsetEndVertex(vertex);
                }
                else if (Start.IsDefault)
                {
                    SetStartVertex(vertex);
                }
                else if (!Start.IsDefault && End.IsDefault)
                {
                    SetEndVertex(vertex);
                }
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

        public bool HasEndPointsSet()
        {
            return !Start.IsDefault && !End.IsDefault;
        }

        protected abstract void SubscribeVertex(IVertex vertex);
        protected abstract void UnsubscribeVertex(IVertex vertex);

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
    }
}
