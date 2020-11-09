using GraphLib.EventHolder;
using GraphLib.Vertex.Interface;
using System;
using System.Windows.Forms;
using WinFormsVersion.Model;

namespace WinFormsVersion.EventHolder
{
    internal class WinFormsVertexEventHolder : BaseVertexEventHolder
    {
        protected override int GetWheelDelta(EventArgs e)
        {
            return (e as MouseEventArgs).Delta;
        }

        public override void ReversePolarity(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Right)
            {
                base.ReversePolarity(sender, e);
            }
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            if (vertex.IsDefault)
                return;

            (vertex as WinFormsVertex).MouseClick += ChooseExtremeVertices;
            (vertex as WinFormsVertex).MouseClick += ReversePolarity;
            (vertex as WinFormsVertex).MouseWheel += ChangeVertexValue;
        }

        public override void ChooseExtremeVertices(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                base.ChooseExtremeVertices(sender, e);
            }
        }
    }
}
