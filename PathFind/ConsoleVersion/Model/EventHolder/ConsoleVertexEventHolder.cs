using ConsoleVersion.InputClass;
using GraphLibrary.VertexEventHolder;
using ConsoleVersion.Vertex;
using System;
using GraphLibrary.Collection;
using GraphLibrary.Common.Constants;
using GraphLibrary.Vertex;
using GraphLibrary.Extensions;

namespace ConsoleVersion.EventHolder
{
    internal class ConsoleVertexEventHolder : AbstractVertexEventHolder
    {

        public override void ChangeVertexValue(object sender, EventArgs e) =>
            (sender as ConsoleVertex).Cost = Input.InputNumber(ConsoleVersionResources.NewTopValueMsg,
                VertexValueRange.UpperValue, VertexValueRange.LowerValue);

        protected override int GetWheelDelta(EventArgs e)
        {
            return 0;
        }

        protected override void ChargeVertex(IVertex vertex)
        {

        }

        protected override void RefreshVertex(IVertex vertex)
        {
            if (!vertex.IsObstacle)
                vertex.SetToDefault();
        }
    }
}
