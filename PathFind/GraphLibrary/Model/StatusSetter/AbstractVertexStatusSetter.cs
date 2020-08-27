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

        public abstract void ChangeVertexValue(object sender, EventArgs e);

        private void MakeObstacle(ref IVertex vertex)
        {
            if (vertex.IsSimpleVertex)
            {
                BoundSetter.BreakBoundsBetweenNeighbours(vertex);
                vertex.IsObstacle = false;
                vertex.SetToDefault();
                vertex.MarkAsObstacle();
            }
        }

        private void MakeVertex(ref IVertex vertex)
        {           
            vertex.IsObstacle = false;
            vertex.MarkAsSimpleVertex();
            var rand = new Random();
            vertex.Text = rand.GetRandomVertexValue();
            var setter = new NeigbourSetter(graph);
            setter.SetNeighbours(vertex);
            BoundSetter.SetBoundsBetweenNeighbours(vertex);
        }

        protected void Reverse(ref IVertex top)
        {
            if (top.IsObstacle)
                MakeVertex(ref top);
            else
                MakeObstacle(ref top);
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
            Reverse(ref vertex);
        }
    }
}
