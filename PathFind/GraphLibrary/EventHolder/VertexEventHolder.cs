using System;
using GraphLibrary.EventHolder.Interface;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.Vertex;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.ValueRanges;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.VertexConnecting;
using System.Linq;

namespace GraphLibrary.EventHolder
{
    public abstract class VertexEventHolder : IVertexEventHolder
    {
        public IGraph Graph { get; set; }

        public virtual void ChangeVertexValue(object sender, EventArgs e)
        {
            var vertex = sender as IVertex;
            if (vertex.IsObstacle)
                return;
            vertex.Cost = Range.VertexCostRange.ReturnInBounds(vertex.Cost += GetWheelDelta(e) > 0 ? 1 : -1);
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
                MakeVertex(vertex);
            else
                MakeObstacle(vertex);
        }

        public void SubscribeVertices() => SetEventsToVertex(ChargeVertex);

        public virtual void ChooseExtremeVertices(object sender, EventArgs e)
        {
            if (!IsStartChosen())
                SetStartVertex(sender as IVertex);
            else if (IsStartChosen() && !IsEndChosen())
                SetDestinationVertex(sender as IVertex);
        }

        protected abstract void ChargeVertex(IVertex vertex);
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

        private bool IsStartChosen() => !ReferenceEquals(Graph.Start, NullVertex.Instance);

        private bool IsEndChosen() => !ReferenceEquals(Graph.End, NullVertex.Instance);

        private void SetEventsToVertex(Action<IVertex> action)
        {
            Graph.AsParallel().ForAll(action);
        }
    }
}
