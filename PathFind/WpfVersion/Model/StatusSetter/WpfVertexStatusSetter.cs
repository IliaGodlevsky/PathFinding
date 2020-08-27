using GraphLibrary.Extensions;
using GraphLibrary.Graph;
using GraphLibrary.StatusSetter;
using GraphLibrary.Vertex;
using System;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model.StatusSetter
{
    public class WpfVertexStatusSetter : AbstractVertexStatusSetter
    {
        public WpfVertexStatusSetter(AbstractGraph graph) : base(graph)
        {
        }

        public override void ChangeVertexValue(object sender, EventArgs e)
        {
            
        }

        public override void SetDestinationVertex(object sender, EventArgs e)
        {
            base.SetDestinationVertex(sender, e);
            if ((sender as IVertex).IsIsolated())
                return;
            foreach (var butt in graph)
                (butt as WpfVertex).MouseLeftButtonDown -= SetDestinationVertex;
        }

        public override void SetStartVertex(object sender, EventArgs e)
        {
            base.SetStartVertex(sender, e);
            if ((sender as IVertex).IsIsolated())
                return;
            foreach (var butt in graph)
            {
                (butt as WpfVertex).MouseLeftButtonDown -= SetStartVertex;
                (butt as WpfVertex).MouseLeftButtonDown += SetDestinationVertex;
            }
        }
    }
}
