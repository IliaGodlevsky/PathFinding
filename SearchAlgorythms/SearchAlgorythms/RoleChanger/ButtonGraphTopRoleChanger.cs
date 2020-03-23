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

        public event EventHandler SwitchRole;

        public ButtonGraphTopRoleChanger(IGraph graph)
        {
            this.graph = graph;
        }

        public void MakeObstacle(ref IGraphTop top)
        {
            if (top.IsSimpleTop)
            {
                Point previousLocation = (top as GraphTop).Location;
                boundSetter.BreakBoundsBetweenNeighbours((top as GraphTop));
                top = new GraphTop { Location = previousLocation };
                top.MarkAsObstacle();
                graph.Insert(top);
            }
        }

        public void MakeTop(ref IGraphTop top)
        {
            Random rand = new Random();
            Point previousLocation = (top as GraphTop).Location;
            top = new GraphTop { Location = previousLocation };
            if (graph.Start == null)
                (top as GraphTop).Click += SetStartPoint;
            else if (graph.Start != null && graph.End == null)
                (top as GraphTop).Click += SetDestinationPoint;
            (top as GraphTop).Text = (rand.Next(9) + 1).ToString();
            top.IsObstacle = false;
            top.MarkAsGraphTop();
            graph.Insert(top);
            boundSetter.SetBoundsBetweenNeighbours(top as GraphTop);
        }

        public void Reverse(ref IGraphTop top)
        {
            Size size = (top as GraphTop).Size;
            if (top.IsObstacle)
                MakeTop(ref top);
            else
                MakeObstacle(ref top);
            (top as GraphTop).Size = size;
            (top as GraphTop).MouseDown += new MouseEventHandler(SwitchRole);
        }

        public void SetStartPoint(object sender, EventArgs e)
        {
            GraphTop top = sender as GraphTop;
            if (!IsRightDestination(top))
                return;
            top.IsStart = true;
            foreach (var butt in graph)
            {
                (butt as GraphTop).Click -= SetStartPoint;
                (butt as GraphTop).Click += SetDestinationPoint;
            }
            top.MarkAsStart();
            graph.Start = top;
        }

        public void SetDestinationPoint(object sender, EventArgs e)
        {
            GraphTop top = sender as GraphTop;
            if (!IsRightDestination(top))
                return;
            top.IsEnd = true;
            top.MarkAsEnd();
            foreach (var butt in graph)
                (butt as GraphTop).Click -= SetDestinationPoint;
            graph.End = top;
        }

        private bool IsRightDestination(GraphTop top)
        {
            return !top.Neighbours.IsEmpty() && top.IsSimpleTop;
        }
    }
}
