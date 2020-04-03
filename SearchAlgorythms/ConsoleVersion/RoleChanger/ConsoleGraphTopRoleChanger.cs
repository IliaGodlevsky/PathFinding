using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;
using System;
using System.Linq;

namespace SearchAlgorythms.RoleChanger
{
    public class ConsoleGraphTopRoleChanger : IGraphTopRoleChanger
    {
        private IGraph graph;

        public ConsoleGraphTopRoleChanger(IGraph graph)
        {
            this.graph = graph;
        }

        public void ChangeTopText(object sender, EventArgs e)
        {
            return;
        }

        public void ReversePolarity(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
    }
}
