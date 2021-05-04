using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;
using System.Windows.Forms;

namespace WindowsFormsVersion.Model
{
    internal sealed class VertexEventHolder : BaseVertexEventHolder
    {
        public VertexEventHolder(IVertexCostFactory costFactory) : base(costFactory)
        {
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return e is MouseEventArgs args ? args.Delta : default;
        }

        public override void Reverse(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs)?.Button == MouseButtons.Right)
            {
                base.Reverse(sender, e);
            }
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseClick += Reverse;
                vert.MouseWheel += ChangeVertexCost;
            }
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseClick -= Reverse;
                vert.MouseWheel -= ChangeVertexCost;
            }
        }
    }
}
