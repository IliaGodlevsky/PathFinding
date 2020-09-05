using GraphLibrary.Extensions;
using GraphLibrary.VertexEventHolder;
using GraphLibrary.Vertex;
using System;
using System.Windows.Input;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model.EventHolder
{
    internal class WpfVertexEventHolder : AbstractVertexEventHolder
    {
        protected override int GetWheelDelta(EventArgs e)
        {
            return (e as MouseWheelEventArgs).Delta;
        }

        protected override void ChargeVertex(IVertex vertex)
        {
            (vertex as WpfVertex).MouseLeftButtonDown += ChooseExtremeVertices;           
            (vertex as WpfVertex).MouseRightButtonDown += ReversePolarity;
            (vertex as WpfVertex).MouseWheel += ChangeVertexValue;
        }
    }
}
