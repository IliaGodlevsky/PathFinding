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
            var vertex = sender as IVertex;
            if (vertex.IsObstacle)
                return;
            int delta = GetWheelDelta(e) > 0 ? 1 : -1;
            int newCost = vertex.Cost + delta;
            int boundedCost = Range.VertexCostRange.ReturnInBounds(newCost);
            vertex.Cost = new VertexCost(boundedCost);
        }

        public virtual void SetStartVertex(IVertex vertex)
        {
            if (!vertex.IsValidToBeRange())
                return;
            vertex.MarkAsStart();
            Graph.Start = vertex;
        }

        public virtual void SetDestinationVertex(IVertex vertex)
        {
            if (!vertex.IsValidToBeRange())
                return;
            vertex.MarkAsEnd();
            Graph.End = vertex;
        }

        public virtual void ReversePolarity(object sender, EventArgs e)
        {
            var vertex = sender as IVertex;

            if (vertex.IsObstacle)
            {
                MakeVertex(vertex);
            }
            else
            {
                MakeObstacle(vertex);
            }
        }

        public void SubscribeVertices()
        {
            SetEventsToVertex(SubscribeToEvents);
        }

        public virtual void ChooseExtremeVertices(object sender, EventArgs e)
        {
            if (!IsStartChosen())
            {
                SetStartVertex(sender as IVertex);
            }
            else if (IsStartChosen() && !IsEndChosen())
            {
                SetDestinationVertex(sender as IVertex);
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

        private bool IsStartChosen() => !Graph.Start.IsDefault;

        private bool IsEndChosen() => !Graph.End.IsDefault;

        private void SetEventsToVertex(Action<IVertex> action)
        {
            Graph.AsParallel().ForAll(action);
        }
    }
}
