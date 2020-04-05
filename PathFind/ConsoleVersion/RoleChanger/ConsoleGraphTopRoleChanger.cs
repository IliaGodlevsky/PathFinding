using ConsoleVersion.InputClass;
using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;
using System;
using System.Linq;

namespace SearchAlgorythms.RoleChanger
{
    public class ConsoleGraphTopRoleChanger : IGraphTopRoleChanger
    {
        private AbstractGraph graph;

        public ConsoleGraphTopRoleChanger(AbstractGraph graph)
        {
            this.graph = graph;
        }

        public void ChangeTopText(object sender, EventArgs e)
        {
            (sender as ConsoleGraphTop).Text = 
                Input.InputNumber("Enter new top value: ", 9, 1).ToString();
        }

        public void ReversePolarity(object sender, EventArgs e)
        {
            IGraphTop top = sender as ConsoleGraphTop;
            Reverse(ref top);
        }

        public void SetDestinationPoint(object sender, EventArgs e)
        {
            ConsoleGraphTop top = sender as ConsoleGraphTop;
            if (!IsRightDestination(top))
                return;
            top.IsEnd = true;
            top.MarkAsEnd();
            graph.End = top;
        }

        public void SetStartPoint(object sender, EventArgs e)
        {
            ConsoleGraphTop top = sender as ConsoleGraphTop;
            if (!IsRightDestination(top))
                return;
            top.IsStart = true;
            top.MarkAsStart();
            graph.Start = top;
        }

        private bool IsRightDestination(ConsoleGraphTop top)
        {
            return top.Neighbours.Any() && top.IsSimpleTop
                && !top.IsObstacle;
        }

        private void MakeObstacle(ref IGraphTop top)
        {
            if (top.IsSimpleTop)
            {
                BoundSetter.BreakBoundsBetweenNeighbours(top);
                top.IsObstacle = false;
                top.SetToDefault();
                top.MarkAsObstacle();
            }
        }

        private void MakeTop(ref IGraphTop top)
        {
            Random rand = new Random();
            top.IsObstacle = false;
            top.MarkAsGraphTop();
            (top as ConsoleGraphTop).Text = (rand.Next(9) + 1).ToString();
            NeigbourSetter setter = new NeigbourSetter(graph.GetArray());
            var coordinates = (graph as ConsoleGraph).GetIndexes(top);
            setter.SetNeighbours(coordinates.X, coordinates.Y);
            BoundSetter.SetBoundsBetweenNeighbours(top);
        }

        private void Reverse(ref IGraphTop top)
        {
            if (top.IsObstacle)
                MakeTop(ref top);
            else
                MakeObstacle(ref top);
        }
    }
}
