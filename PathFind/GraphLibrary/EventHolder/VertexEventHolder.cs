using System;
using GraphLibrary.EventHolder.Interface;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.Vertex;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.ValueRanges;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.VertexConnecting;

namespace GraphLibrary.EventHolder
{
    public abstract class VertexEventHolder : IVertexEventHolder
    {
        public IGraph Graph { get; set; }

        protected abstract int GetWheelDelta(EventArgs e);

        public virtual void ChangeVertexValue(object sender, EventArgs e)
        {
            var vertex = sender as IVertex;
            if (vertex.IsObstacle)
                return;
            vertex.Cost = Range.VertexCostRange.ReturnInBounds(vertex.Cost += GetWheelDelta(e) > 0 ? 1 : -1);
        }

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

        public void ChargeGraph() => SetEventsToVertex(ChargeVertex);

        protected abstract void ChargeVertex(IVertex vertex);

        private void SetEventsToVertex(Action<IVertex> action)
        {
            Graph.Array.Apply(vertex => { action(vertex); return vertex; });
        }

        public virtual void ChooseExtremeVertices(object sender, EventArgs e)
        {
            if (ReferenceEquals(Graph.Start, NullVertex.Instance))
                SetStartVertex(sender as IVertex);
            else if (!ReferenceEquals(Graph.Start, NullVertex.Instance)
                && ReferenceEquals(Graph.End, NullVertex.Instance))
                SetDestinationVertex(sender as IVertex);
        }
    }
}
