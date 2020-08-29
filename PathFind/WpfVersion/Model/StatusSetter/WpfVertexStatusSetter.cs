using GraphLibrary.Extensions;
using GraphLibrary.Graph;
using GraphLibrary.StatusSetter;
using GraphLibrary.Vertex;
using System;
using System.Windows.Input;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model.StatusSetter
{
    internal class WpfVertexStatusSetter : AbstractVertexStatusSetter
    {

        public WpfVertexStatusSetter(AbstractGraph graph) : base(graph)
        { 

        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return (e as MouseWheelEventArgs).Delta;
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
