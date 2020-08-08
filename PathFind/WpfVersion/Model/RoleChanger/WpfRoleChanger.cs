using GraphLibrary.Graph;
using GraphLibrary.RoleChanger;
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

        public override void SetDestinationPoint(object sender, EventArgs e)
        {
            base.SetDestinationPoint(sender, e);
            foreach (var butt in graph)
                (butt as WpfVertex).MouseLeftButtonDown -= SetDestinationPoint;
        }

        public override void SetStartPoint(object sender, EventArgs e)
        {
            base.SetStartPoint(sender, e);
            foreach (var butt in graph)
            {
                (butt as WpfVertex).MouseLeftButtonDown -= SetStartPoint;
                (butt as WpfVertex).MouseLeftButtonDown += SetDestinationPoint;
            }

        }
    }
}
