using GraphLib.Base.EventHolder;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;

namespace WPFVersion3D.Model
{
    internal sealed class Vertex3DEventHolder : BaseVertexEventHolder, IVertexEventHolder
    {
        public Vertex3DEventHolder(IVertexCostFactory costFactory) : base(costFactory)
        {
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return 0;
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            if (vertex is Vertex3D vertex3D)
            {
                vertex3D.MouseRightButtonDown += Reverse;
            }
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            if (vertex is Vertex3D vertex3D)
            {
                vertex3D.MouseRightButtonDown -= Reverse;
            }
        }
    }
}
