using GraphLibrary.Constants;
using GraphLibrary.Extensions;
using GraphLibrary.Graph;
using GraphLibrary.StatusSetter;
using GraphLibrary.Vertex;
using System;
using System.Windows.Input;
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
            var vertex = (sender as WpfVertex);
            if (vertex.IsObstacle)
                return;
            int current = int.Parse(vertex.Text);
            if ((e as MouseWheelEventArgs).Delta > 0)
                current++;
            else
                current--;
            if (current > Const.MAX_VERTEX_VALUE)
                current = Const.MIN_VERTEX_VALUE;
            else if (current < Const.MIN_VERTEX_VALUE)
                current = Const.MAX_VERTEX_VALUE;
            vertex.Text = current.ToString();
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
