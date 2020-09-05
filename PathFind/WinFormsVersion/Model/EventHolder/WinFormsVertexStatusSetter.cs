using System;
using System.Windows.Forms;
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

        public override void ReversePolarity(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Right)
                base.ReversePolarity(sender, e);            
        }

        protected override void ChargeVertex(IVertex vertex)
        {
            (vertex as WinFormsVertex).MouseClick += ChooseExtremeVertices;
            (vertex as WinFormsVertex).MouseClick += ReversePolarity;
            (vertex as WinFormsVertex).MouseWheel += ChangeVertexValue;
        }

        public override void ChooseExtremeVertices(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                if (Graph.Start == null)
                    SetStartVertex(sender as IVertex);
                else if (Graph.Start != null && Graph.End == null)
                    SetDestinationVertex(sender as IVertex);
            }
        }
    }
}
