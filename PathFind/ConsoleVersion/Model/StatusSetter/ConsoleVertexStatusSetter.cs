using ConsoleVersion.InputClass;
using GraphLibrary.VertexEventHolder;
using ConsoleVersion.Vertex;
using System;
using GraphLibrary.Collection;
using GraphLibrary.Common.Constants;

namespace ConsoleVersion.StatusSetter
{
    internal class ConsoleVertexStatusSetter : AbstractVertexEventHolder
    {

        public ConsoleVertexStatusSetter(GraphLibrary.Collection.Graph graph) : base(graph)
        {

        }

        public override void ChangeVertexValue(object sender, EventArgs e) =>
            (sender as ConsoleVertex).Cost = Input.InputNumber(ConsoleVersionResources.NewTopValueMsg,
                VertexValueRange.UpperValue, VertexValueRange.LowerValue);

        protected override int GetWheelDelta(EventArgs e)
        {
            return default;
        }
    }
}
