using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SearchAlgorythms.Algorythms;
using SearchAlgorythms.Extensions;
using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.RoleChanger
{
    public class ButtonGraphTopRoleChanger : IGraphTopRoleChanger
    {
        private IGraph graph;
        private BoundSetter boundSetter = new BoundSetter();

        public ButtonGraphTopRoleChanger(IGraph graph)
        {
            this.graph = graph;
        }

        public void ChangeTopText(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Middle)
            {
                if ((sender as GraphTop).IsObstacle)
                    return;
                int current = int.Parse((sender as GraphTop).Text);
                current++;
                if (current > 9)
                    current = 1;
                (sender as GraphTop).Text = current.ToString();
            }
        }

        private void MakeObstacle(ref IGraphTop top)
        {
            if (top.IsSimpleTop)
            {
                boundSetter.BreakBoundsBetweenNeighbours(top);
                top.Neighbours.Clear();
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
            (top as GraphTop).Text = (rand.Next(9) + 1).ToString();
            NeigbourSetter setter = new NeigbourSetter(graph.GetArray());
            var coordinates = (graph as ButtonGraph).GetIndexes(top);
            setter.SetNeighbours(coordinates.X, coordinates.Y);
            boundSetter.SetBoundsBetweenNeighbours(top);
        }

        private void Reverse(ref IGraphTop top)
        {
            if (top.IsObstacle)
                MakeTop(ref top);
            else
                MakeObstacle(ref top);
        }

        public void SetStartPoint(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                GraphTop top = sender as GraphTop;
                if (!IsRightDestination(top))
                    return;
                top.IsStart = true;
                foreach (var butt in graph)
                {
                    (butt as GraphTop).MouseClick -= SetStartPoint;
                    (butt as GraphTop).MouseClick += SetDestinationPoint;
                }
                top.MarkAsStart();
                graph.Start = top;
            }
        }

        public void SetDestinationPoint(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                GraphTop top = sender as GraphTop;
                if (!IsRightDestination(top))
                    return;
                top.IsEnd = true;
                top.MarkAsEnd();
                foreach (var butt in graph)
                    (butt as GraphTop).MouseClick -= SetDestinationPoint;
                graph.End = top;
            }
        }

        private bool IsRightDestination(GraphTop top)
        {
            return !top.Neighbours.IsEmpty() && top.IsSimpleTop
                && !top.IsObstacle;
        }

        public void ReversePolarity(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Right)
            {
                IGraphTop top = sender as GraphTop;
                Reverse(ref top);
            }
        }
    }
}
