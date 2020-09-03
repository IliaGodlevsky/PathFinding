using GraphLibrary.Common.Constants;
using GraphLibrary.Extensions;
using GraphLibrary.Collection;
using GraphLibrary.Vertex;
using System;

namespace GraphLibrary.VertexEventHolder
{
    public abstract class AbstractVertexEventHolder : IVertexEventHolder
    {
        protected Graph graph;

        public AbstractVertexEventHolder(Graph graph)
        {
            this.graph = graph;
        }

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
            VertexBinder.SetNeighbours(graph, vertex);
            VertexBinder.ConnectToNeighbours(vertex);
        }

        public virtual void SetStartVertex(object sender, EventArgs e)
        {
            var vertex = sender as IVertex;
            if (!vertex.IsValidToBeRange())
                return;
            vertex.IsStart = true;
            vertex.MarkAsStart();
            graph.Start = vertex;
        }

        public virtual void SetDestinationVertex(object sender, EventArgs e)
        {
            var vertex = sender as IVertex;
            if (!vertex.IsValidToBeRange())
                return;
            vertex.IsEnd = true;
            vertex.MarkAsEnd();
            graph.End = vertex;
        }

        public virtual void ReversePolarity(object sender, EventArgs e)
        {
            var vertex = sender as IVertex;
            if (vertex.IsObstacle)
                MakeVertex(vertex);
            else
                MakeObstacle(vertex);
        }
    }
}
