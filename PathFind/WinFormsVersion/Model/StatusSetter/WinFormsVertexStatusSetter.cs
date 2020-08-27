using System;
using System.Windows.Forms;
using GraphLibrary.Constants;
using GraphLibrary.Extensions;
using GraphLibrary.Graph;
using GraphLibrary.StatusSetter;
using GraphLibrary.Vertex;
using WinFormsVersion.Vertex;

namespace WinFormsVersion.StatusSetter
{
    public class WinFormsVertexStatusSetter : AbstractVertexStatusSetter
    {
        public WinFormsVertexStatusSetter(AbstractGraph graph) : base(graph)
        {

        }

        public override void ChangeVertexValue(object sender, EventArgs e)
        {
            var vertex = (sender as WinFormsVertex);
            if (vertex.IsObstacle)
                return;
            int current = int.Parse(vertex.Text);
            current++;
            if (current > Const.MAX_VERTEX_VALUE)
                current = Const.MIN_VERTEX_VALUE;
            vertex.Text = current.ToString();
        }

        public override void SetStartVertex(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                base.SetStartVertex(sender, e);
                if ((sender as IVertex).IsIsolated())
                    return;
                foreach (var butt in graph)
                {
                    (butt as WinFormsVertex).MouseClick -= SetStartVertex;
                    (butt as WinFormsVertex).MouseClick += SetDestinationVertex;
                }
            }
        }

        public override void SetDestinationVertex(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                base.SetDestinationVertex(sender, e);
                if ((sender as IVertex).IsIsolated())
                    return;
                foreach (var butt in graph) 
                    (butt as WinFormsVertex).MouseClick -= SetDestinationVertex;

            }
        }

        public override void ReversePolarity(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Right)
                base.ReversePolarity(sender, e);            
        }
    }
}
