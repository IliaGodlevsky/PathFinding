using ConsoleVersion.InputClass;
using GraphLibrary.StatusSetter;
using ConsoleVersion.Vertex;
using System;
using GraphLibrary.Graph;
using GraphLibrary.Common.Constants;

namespace ConsoleVersion.StatusSetter
{
    internal class ConsoleVertexStatusSetter : AbstractVertexStatusSetter
    {

        public ConsoleVertexStatusSetter(AbstractGraph graph) : base(graph)
        {

        }

        public override void ChangeVertexValue(object sender, EventArgs e) =>
            (sender as ConsoleVertex).Cost = Input.InputNumber(Res.NewTopValueMsg,
                VertexValueRange.UpperValue, VertexValueRange.LowerValue);

        protected override int GetWheelDelta(EventArgs e)
        {
            return default;
        }
    }
}
