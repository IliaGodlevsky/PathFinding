using GraphLib.Base;
using GraphLib.Interface;
using System;
using System.Windows.Input;

namespace WPFVersion.Model.EventHolder
{
    internal sealed class VertexEventHolder : BaseVertexEventHolder
    {
        protected override int GetWheelDelta(EventArgs e)
        {
            return (e as MouseWheelEventArgs).Delta;
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            (vertex as Vertex).MouseRightButtonDown += Reverse;
            (vertex as Vertex).MouseWheel += ChangeVertexCost;
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            (vertex as Vertex).MouseRightButtonDown -= Reverse;
            (vertex as Vertex).MouseWheel -= ChangeVertexCost;
        }
    }
}
