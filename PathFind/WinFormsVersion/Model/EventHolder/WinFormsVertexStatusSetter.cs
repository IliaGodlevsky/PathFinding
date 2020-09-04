using System;
using System.Windows.Forms;
using GraphLibrary.Extensions;
using GraphLibrary.Collection;
using GraphLibrary.VertexEventHolder;
using GraphLibrary.Vertex;
using WinFormsVersion.Vertex;

namespace WinFormsVersion.EventHolder
{
    internal class WinFormsVertexEventHolder : AbstractVertexEventHolder
    {
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
                foreach (WinFormsVertex vertex in Graph)
                {
                    vertex.MouseClick -= SetStartVertex;
                    vertex.MouseClick += SetDestinationVertex;
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
                foreach (WinFormsVertex vertex in Graph)
                    vertex.MouseClick -= SetDestinationVertex;

            }
        }

        public override void ReversePolarity(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Right)
                base.ReversePolarity(sender, e);            
        }

        protected override void ChargeVertex(IVertex vertex)
        {
            if (!vertex.IsObstacle)
                (vertex as WinFormsVertex).MouseClick += SetStartVertex;
            (vertex as WinFormsVertex).MouseClick += ReversePolarity;
            (vertex as WinFormsVertex).MouseWheel += ChangeVertexValue;
        }

        protected override void RefreshVertex(IVertex vertex)
        {
            if (!vertex.IsObstacle)
            {
                vertex.SetToDefault();
                (vertex as WinFormsVertex).MouseClick -= SetStartVertex;
                (vertex as WinFormsVertex).MouseClick -= SetDestinationVertex;
                (vertex as WinFormsVertex).MouseClick += SetStartVertex;
            }
        }
    }
}
