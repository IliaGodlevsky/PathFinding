using GraphLibrary.Graph;
using GraphLibrary.Vertex;
using System;
using System.Linq;

namespace GraphLibrary.RoleChanger
{
    public abstract class AbstractVertexRoleChanger : IVertexRoleChanger
    {
        protected AbstractGraph graph;

        public AbstractVertexRoleChanger(AbstractGraph graph)
        {
            this.graph = graph;
        }

        public abstract void ChangeTopText(object sender, EventArgs e);

        private void MakeObstacle(ref IVertex top)
        {
            if (top.IsSimpleVertex)
            {
                BoundSetter.BreakBoundsBetweenNeighbours(top);
                top.IsObstacle = false;
                top.SetToDefault();
                top.MarkAsObstacle();
            }
        }

        private void MakeTop(ref IVertex top)
        {
            Random rand = new Random();
            top.IsObstacle = false;
            top.MarkAsSimpleVertex();
            top.Text = (rand.Next(9) + 1).ToString();
            NeigbourSetter setter = new NeigbourSetter(graph.GetArray());
            var coordinates = graph.GetIndexes(top);
            setter.SetNeighbours(coordinates.X, coordinates.Y);
            BoundSetter.SetBoundsBetweenNeighbours(top);
        }

        protected void Reverse(ref IVertex top)
        {
            if (top.IsObstacle)
                MakeTop(ref top);
            else
                MakeObstacle(ref top);
        }

        public abstract void SetStartPoint(object sender, EventArgs e);

        public abstract void SetDestinationPoint(object sender, EventArgs e);

        protected bool IsRightDestination(IVertex vertex)
        {
            return vertex.Neighbours.Any() && vertex.IsSimpleVertex
                && !vertex.IsObstacle;
        }

        public abstract void ReversePolarity(object sender, EventArgs e);
    }
}
