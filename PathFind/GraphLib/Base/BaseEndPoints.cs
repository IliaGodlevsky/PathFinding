using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.NullObjects;
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
            graph.AsParallel().ForAll(SubscribeVertex);
        }       

        public bool IsEndPoint(IVertex vertex)
        {
            return vertex.IsEqual(Start) || vertex.IsEqual(End);
        }

        public virtual void SetEndPoints(object sender, EventArgs e)
        {
            if (sender is IVertex vertex)
            {
                if (Start.IsDefault)
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

        protected virtual void SetStartVertex(IVertex vertex)
        {
            if (CanBeEndPoint(vertex))
            {
                Start = vertex;
                Start.MarkAsStart();
            }
        }

        protected virtual void SetEndVertex(IVertex vertex)
        {
            if (CanBeEndPoint(vertex))
            {
                End = vertex;
                End.MarkAsEnd();
            }
        }
    }
}
