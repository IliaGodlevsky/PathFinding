using ConsoleVersion.InputClass;
using System;
using GraphLibrary.Common.Constants;
using GraphLibrary.EventHolder;
using GraphLibrary.Vertex.Interface;
using ConsoleVersion.Model.Vertex;

namespace ConsoleVersion.Model.EventHolder
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
    }
}
