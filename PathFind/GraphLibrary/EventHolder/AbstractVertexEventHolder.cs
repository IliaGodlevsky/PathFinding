using System;
using GraphLibrary.EventHolder.Interface;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.Vertex;
using GraphLibrary.VertexBinding;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.ValueRanges;
using GraphLibrary.Extensions.SystemTypeExtensions;

namespace GraphLibrary.EventHolder
{
    public abstract class AbstractVertexEventHolder : IVertexEventHolder
    {
        public Graph Graph { get; set; }

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
            if (vertex.IsSimpleVertex())
            {
                VertexBinder.IsolateVertex(vertex);
                vertex.IsObstacle = false;
                vertex.SetToDefault();
                vertex.MarkAsObstacle();
            }
        }

        private void MakeVertex(IVertex vertex)
        {
            vertex.IsObstacle = false;
            vertex.MarkAsSimpleVertex();
            VertexBinder.SetNeighbours(Graph, vertex);
            VertexBinder.ConnectToNeighbours(vertex);
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
            Graph.Array.ApplyParallel(vertex => { action(vertex); return vertex; });
        }

        public virtual void ChooseExtremeVertices(object sender, EventArgs e)
        {
            var nullVertex = NullVertex.Instance;
            if (Graph.Start == nullVertex)
                SetStartVertex(sender as IVertex);
            else if (Graph.Start != nullVertex && Graph.End == nullVertex)
                SetDestinationVertex(sender as IVertex);
        }
    }
}
