using GraphLibrary.Common.Constants;
using GraphLibrary.Extensions;
using GraphLibrary.Collection;
using GraphLibrary.Vertex;
using System;

namespace GraphLibrary.VertexEventHolder
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
            vertex.Cost = VertexValueRange.ReturnValueInValueRange(vertex.Cost += GetWheelDelta(e) > 0 ? 1 : -1);
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

        public virtual void SetStartVertex(object sender, EventArgs e)
        {
            var vertex = sender as IVertex;
            if (!vertex.IsValidToBeRange())
                return;
            vertex.IsStart = true;
            vertex.MarkAsStart();
            Graph.Start = vertex;
        }

        public virtual void SetDestinationVertex(object sender, EventArgs e)
        {
            var vertex = sender as IVertex;
            if (!vertex.IsValidToBeRange())
                return;
            vertex.IsEnd = true;
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

        public void RefreshGraph()
        {
            Graph.End = null;
            Graph.Start = null;
            SetEventsToVertex(RefreshVertex);
        }

        protected abstract void ChargeVertex(IVertex vertex);
        protected abstract void RefreshVertex(IVertex vertex);

        private void SetEventsToVertex(Action<IVertex> action)
        {
            for (int i = 0; i < Graph.Width; i++)
                for (int j = 0; j < Graph.Height; j++)
                    action(Graph[i, j]);
        }
    }
}
