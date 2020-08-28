using GraphLibrary.Constants;
using GraphLibrary.Extensions;
using GraphLibrary.Extensions.RandomExtension;
using GraphLibrary.Graph;
using GraphLibrary.Vertex;
using System;

namespace GraphLibrary.StatusSetter
{
    public abstract class AbstractVertexStatusSetter : IVertexStatusSetter
    {
        protected AbstractGraph graph;

        public AbstractVertexStatusSetter(AbstractGraph graph)
        {
            this.graph = graph;
        }

        protected abstract int GetWheelDelta(EventArgs e);
        public virtual void ChangeVertexValue(object sender, EventArgs e)
        {
            var vertex = (sender as IVertex);
            if (vertex.IsObstacle)
                return;
            int current = int.Parse(vertex.Text);
            current += GetWheelDelta(e) > 0 ? 1 : -1;
            if (current > Const.MAX_VERTEX_VALUE)
                current = Const.MIN_VERTEX_VALUE;
            else if (current < Const.MIN_VERTEX_VALUE)
                current = Const.MAX_VERTEX_VALUE;
            vertex.Text = current.ToString();
        }

        private void MakeObstacle(IVertex vertex)
        {
            if (vertex.IsSimpleVertex)
            {
                VertexLinkManager.IsolateVertex(vertex);
                vertex.IsObstacle = false;
                vertex.SetToDefault();
                vertex.MarkAsObstacle();
            }
        }

        private void MakeVertex(IVertex vertex)
        {           
            vertex.IsObstacle = false;
            vertex.MarkAsSimpleVertex();
            var rand = new Random();
            vertex.Text = rand.GetRandomVertexValue();
            VertexLinkManager.SetNeighbours(graph, vertex);
            VertexLinkManager.ConnectToNeighbours(vertex);
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
            IVertex vertex = sender as IVertex;
            if (vertex.IsObstacle)
                MakeVertex(vertex);
            else
                MakeObstacle(vertex);
        }
    }
}
