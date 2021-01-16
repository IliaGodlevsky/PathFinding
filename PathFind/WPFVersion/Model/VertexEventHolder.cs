using GraphLib.EventHolder;
using GraphLib.Vertex.Interface;
using System;
using System.Windows.Input;

namespace WPFVersion.Model.EventHolder
{
    internal class VertexEventHolder : BaseVertexEventHolder
    {
        protected override int GetWheelDelta(EventArgs e)
        {
            return (e as MouseWheelEventArgs).Delta;
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            (vertex as Vertex).MouseLeftButtonDown += ChooseExtremeVertices;
            (vertex as Vertex).MouseRightButtonDown += Reverse;
            (vertex as Vertex).MouseWheel += ChangeVertexCost;
        }
    }
}
