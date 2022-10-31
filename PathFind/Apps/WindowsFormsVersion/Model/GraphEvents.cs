using GraphLib.Base.EventHolder;
using GraphLib.Interfaces.Factories;
using System;
using System.Windows.Forms;

namespace WindowsFormsVersion.Model
{
    internal sealed class GraphEvents : BaseGraphEvents<Vertex>
    {
        public GraphEvents(IVertexCostFactory costFactory) : base(costFactory)
        {
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return e is MouseEventArgs args ? args.Delta > 0 ? 1 : -1 : default;
        }

        protected override void Reverse(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs)?.Button == MouseButtons.Right)
            {
                base.Reverse(sender, e);
            }
        }

        protected override void SubscribeToEvents(Vertex vertex)
        {
            vertex.MouseClick += Reverse;
            vertex.MouseWheel += ChangeVertexCost;
        }

        protected override void UnsubscribeFromEvents(Vertex vertex)
        {
            vertex.MouseClick -= Reverse;
            vertex.MouseWheel -= ChangeVertexCost;
        }
    }
}
