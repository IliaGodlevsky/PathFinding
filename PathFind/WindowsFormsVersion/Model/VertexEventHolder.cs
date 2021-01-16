using GraphLib.EventHolder;
using GraphLib.Vertex.Interface;
using System;
using System.Windows.Forms;
using WindowsFormsVersion.Model;

namespace WindowsFormsVersion.EventHolder
{
    internal class VertexEventHolder : BaseVertexEventHolder
    {
        protected override int GetWheelDelta(EventArgs e)
        {
            return (e as MouseEventArgs).Delta;
        }

        public override void Reverse(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Right)
            {
                base.Reverse(sender, e);
            }
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            if (vertex.IsDefault)
                return;

            (vertex as Vertex).MouseClick += ChooseExtremeVertices;
            (vertex as Vertex).MouseClick += Reverse;
            (vertex as Vertex).MouseWheel += ChangeVertexCost;
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
