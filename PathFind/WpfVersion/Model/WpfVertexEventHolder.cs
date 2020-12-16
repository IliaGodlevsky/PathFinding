using GraphLib.EventHolder;
using GraphLib.Vertex.Interface;
using System;
using System.Windows.Input;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model.EventHolder
{
    internal class WpfVertexEventHolder : BaseVertexEventHolder
    {
        protected override int GetWheelDelta(EventArgs e)
        {
            return (e as MouseWheelEventArgs).Delta;
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            (vertex as WpfVertex).MouseLeftButtonDown += ChooseExtremeVertices;
            (vertex as WpfVertex).MouseRightButtonDown += Reverse;
            (vertex as WpfVertex).MouseWheel += ChangeVertexCost;
        }
    }
}
