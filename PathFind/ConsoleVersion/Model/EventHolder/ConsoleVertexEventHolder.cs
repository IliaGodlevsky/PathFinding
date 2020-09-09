using ConsoleVersion.InputClass;
using System;
using GraphLibrary.EventHolder;
using GraphLibrary.Vertex.Interface;
using ConsoleVersion.Model.Vertex;
using GraphLibrary.ValueRanges;

namespace ConsoleVersion.Model.EventHolder
{
    internal class ConsoleVertexEventHolder : AbstractVertexEventHolder
    {

        public override void ChangeVertexValue(object sender, EventArgs e) =>
            (sender as ConsoleVertex).Cost = Input.InputNumber(ConsoleVersionResources.NewTopValueMsg,
                Range.VertexCostRange.UpperRange, Range.VertexCostRange.LowerRange);

        protected override int GetWheelDelta(EventArgs e)
        {
            return 0;
        }

        protected override void ChargeVertex(IVertex vertex)
        {

        }
    }
}
