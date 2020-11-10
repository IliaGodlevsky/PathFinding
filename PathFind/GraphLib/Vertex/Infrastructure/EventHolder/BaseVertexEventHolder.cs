using System;
using GraphLib.EventHolder.Interface;
using GraphLib.Vertex.Interface;
using GraphLib.VertexConnecting;
using System.Linq;
using GraphLib.Vertex.Cost;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using Common.ValueRanges;

namespace GraphLib.EventHolder
{
    public abstract class BaseVertexEventHolder : IVertexEventHolder
    {
        public IGraph Graph { get; set; }

        public virtual void ChangeVertexValue(object sender, EventArgs e)
        {
            if (sender is IVertex vertex)
            {
                if (!vertex.IsObstacle)
                {
                    int delta = GetWheelDelta(e) > 0 ? 1 : -1;
                    int newCost = vertex.Cost + delta;
                    int boundedCost = Range.VertexCostRange.ReturnInBounds(newCost);
                    vertex.Cost = new VertexCost(boundedCost);
                }
            }
        }

        public virtual void SetStartVertex(IVertex vertex)
        {
            if (vertex.IsValidToBeRange())
            {
                vertex.MarkAsStart();
                Graph.Start = vertex;
            }
        }

        public virtual void SetDestinationVertex(IVertex vertex)
        {
            if (vertex.IsValidToBeRange())
            {
                vertex.MarkAsEnd();
                Graph.End = vertex;
            }
        }

        public virtual void ReversePolarity(object sender, EventArgs e)
        {
            if (sender is IVertex vertex)
            {
                if (vertex.IsObstacle)
                {
                    MakeVertex(vertex);
                }
                else
                {
                    MakeObstacle(vertex);
                }
            }
        }

        public void SubscribeVertices()
        {
            Graph.AsParallel().ForAll(SubscribeToEvents);
        }

        public virtual void ChooseExtremeVertices(object sender, EventArgs e)
        {
            if (sender is IVertex vertex)
            {
                if (!IsStartChosen())
                {
                    SetStartVertex(vertex);
                }
                else if (IsStartChosen() && !IsEndChosen())
                {
                    SetDestinationVertex(vertex);
                }
            }
        }

        protected abstract void SubscribeToEvents(IVertex vertex);

        protected abstract int GetWheelDelta(EventArgs e);

        private void MakeObstacle(IVertex vertex)
        {
            if (vertex.IsSimpleVertex() && !vertex.IsVisited)
            {
                VertexConnector.IsolateVertex(vertex);
                vertex.IsObstacle = true;
                vertex.SetToDefault();
                vertex.MarkAsObstacle();
            }
        }

        private void MakeVertex(IVertex vertex)
        {
            vertex.IsObstacle = false;
            vertex.MarkAsSimpleVertex();
            VertexConnector.SetNeighbours(Graph, vertex);
            VertexConnector.ConnectToNeighbours(vertex);
        }

        private bool IsStartChosen()
        {
            return !Graph.Start.IsDefault;
        }

        private bool IsEndChosen()
        {
            return !Graph.End.IsDefault;
        }
    }
}
