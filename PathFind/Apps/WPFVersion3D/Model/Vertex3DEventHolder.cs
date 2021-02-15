using GraphLib.Base;
using GraphLib.Interface;
using System;

namespace WPFVersion3D.Model
{
    public class Vertex3DEventHolder : BaseVertexEventHolder
    {
        protected override int GetWheelDelta(EventArgs e)
        {
            return 0;
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {            
            (vertex as Vertex3D).MouseRightButtonDown += Reverse;
            (vertex as Vertex3D).MouseWheel += ChangeVertexCost;
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            (vertex as Vertex3D).MouseRightButtonDown -= Reverse;
            (vertex as Vertex3D).MouseWheel -= ChangeVertexCost;
        }
    }
}
