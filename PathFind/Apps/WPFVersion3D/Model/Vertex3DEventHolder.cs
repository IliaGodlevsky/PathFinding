using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;
using System.Windows.Input;

namespace WPFVersion3D.Model
{
    internal sealed class Vertex3DEventHolder : BaseVertexEventHolder
    {
        public Vertex3DEventHolder(IVertexCostFactory costFactory) : base(costFactory)
        {
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return e is MouseWheelEventArgs args ? args.Delta > 0 ? 1 : -1 : default;
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            if (vertex is Vertex3D vertex3D)
            {
                vertex3D.MouseRightButtonDown += Reverse;
                vertex3D.MouseWheel += ChangeVertexCost;
            }
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            if (vertex is Vertex3D vertex3D)
            {
                vertex3D.MouseRightButtonDown -= Reverse;
                vertex3D.MouseWheel -= ChangeVertexCost;
            }
        }
    }
}
