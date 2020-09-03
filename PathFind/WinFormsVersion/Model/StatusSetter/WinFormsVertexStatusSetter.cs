using System;
using System.Windows.Forms;
using GraphLibrary.Extensions;
using GraphLibrary.Collection;
using GraphLibrary.VertexEventHolder;
using GraphLibrary.Vertex;
using WinFormsVersion.Vertex;

namespace WinFormsVersion.StatusSetter
{
    internal class WinFormsVertexStatusSetter : AbstractVertexEventHolder
    {
        public WinFormsVertexStatusSetter(GraphLibrary.Collection.Graph graph) : base(graph)
        {

        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return (e as MouseEventArgs).Delta;
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
