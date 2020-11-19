using GraphLib.EventHolder;
using GraphLib.Vertex.Interface;
using System;

namespace Wpf3dVersion.Model
{
    public class Wpf3dVertexEventHolder : BaseVertexEventHolder
    {
        protected override int GetWheelDelta(EventArgs e)
        {
            return 0;
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            (vertex as Wpf3dVertex).MouseLeftButtonDown += ChooseExtremeVertices;
            (vertex as Wpf3dVertex).MouseRightButtonDown += Reverse;
            (vertex as Wpf3dVertex).MouseWheel += ChangeVertexCost;
        }
    }
}
