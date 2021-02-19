using GraphLib.Base;
using GraphLib.Interface;
using System;
using System.Windows.Input;

namespace WPFVersion3D.Model
{
    internal class Vertex3DEventHolder : BaseVertexEventHolder
    {
        protected override int GetWheelDelta(EventArgs e)
        {
            return (e as MouseWheelEventArgs).Delta;
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
