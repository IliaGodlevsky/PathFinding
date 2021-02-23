using Common.Extensions;
using GraphLib.Base;
using GraphLib.Interface;
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
            if (vertex.IsDefault())
                return;

            (vertex as Vertex).MouseClick += Reverse;
            (vertex as Vertex).MouseWheel += ChangeVertexCost;
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            if (vertex.IsDefault())
                return;

            (vertex as Vertex).MouseClick -= Reverse;
            (vertex as Vertex).MouseWheel -= ChangeVertexCost;
        }
    }
}
