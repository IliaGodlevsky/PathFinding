using GraphLibrary.Graph;
using GraphLibrary.RoleChanger;
using GraphLibrary.Vertex;
using System;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model.RoleChanger
{
    public class WpfRoleChanger : AbstractVertexRoleChanger
    {
        public WpfRoleChanger(AbstractGraph graph) : base(graph)
        {
        }

        public override void ChangeTopText(object sender, EventArgs e)
        {
            
        }

        public override void ReversePolarity(object sender, EventArgs e)
        {
            IVertex top = sender as WpfVertex;
            Reverse(ref top);
        }

        public override void SetDestinationPoint(object sender, EventArgs e)
        {
            WpfVertex top = sender as WpfVertex;
            if (!IsRightDestination(top))
                return;
            top.IsEnd = true;
            top.MarkAsEnd();
            foreach (var butt in graph)
                (butt as WpfVertex).Click -= SetDestinationPoint;
            graph.End = top;
        }

        public override void SetStartPoint(object sender, EventArgs e)
        {
            WpfVertex top = sender as WpfVertex;
            if (!IsRightDestination(top))
                return;
            top.IsStart = true;
            foreach (var butt in graph)
            {
                (butt as WpfVertex).Click -= SetStartPoint;
                (butt as WpfVertex).Click += SetDestinationPoint;
            }
            top.MarkAsStart();
            graph.Start = top;
        }
    }
}
