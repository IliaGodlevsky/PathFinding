using System;
using System.Windows.Forms;
using GraphLibrary.Graph;
using GraphLibrary.RoleChanger;
using GraphLibrary.Vertex;
using WinFormsVersion.Vertex;

namespace WinFormsVersion.RoleChanger
{
    public class WinFormsVertexRoleChanger : AbstractVertexRoleChanger
    {
        public WinFormsVertexRoleChanger(AbstractGraph graph) : base(graph)
        {

        }

        public override void ChangeTopText(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Middle)
            {
                if ((sender as WinFormsVertex).IsObstacle)
                    return;
                int current = int.Parse((sender as WinFormsVertex).Text);
                current++;
                if (current > 9)
                    current = 1;
                (sender as WinFormsVertex).Text = current.ToString();
            }
        }

        public override void SetStartPoint(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                WinFormsVertex top = sender as WinFormsVertex;
                if (!IsRightDestination(top))
                    return;
                top.IsStart = true;
                foreach (var butt in graph)
                {
                    (butt as WinFormsVertex).MouseClick -= SetStartPoint;
                    (butt as WinFormsVertex).MouseClick += SetDestinationPoint;
                }
                top.MarkAsStart();
                graph.Start = top;
            }
        }

        public override void SetDestinationPoint(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                WinFormsVertex top = sender as WinFormsVertex;
                if (!IsRightDestination(top))
                    return;
                top.IsEnd = true;
                top.MarkAsEnd();
                foreach (var butt in graph)
                    (butt as WinFormsVertex).MouseClick -= SetDestinationPoint;
                graph.End = top;
            }
        }

        public override void ReversePolarity(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Right)
            {
                IVertex top = sender as WinFormsVertex;
                Reverse(ref top);
            }
        }
    }
}
