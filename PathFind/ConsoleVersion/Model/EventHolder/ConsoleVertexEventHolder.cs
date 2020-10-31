using ConsoleVersion.InputClass;
using System;
using GraphLib.EventHolder;
using GraphLib.Vertex.Interface;
using ConsoleVersion.Model.Vertex;
using GraphLib.Vertex.Cost;
using Common.ValueRanges;

namespace ConsoleVersion.Model.EventHolder
{
    internal class ConsoleVertexEventHolder : BaseVertexEventHolder
    {

        public override void ChangeVertexValue(object sender, EventArgs e)
        {
            var vertex = sender as ConsoleVertex;
            if (!vertex.IsObstacle)
                vertex.Cost = new VertexCost(Input.InputNumber(ConsoleVersionResources.NewTopValueMsg,
                    Range.VertexCostRange.UpperRange, Range.VertexCostRange.LowerRange));
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return 0;
        }

        protected override void ChargeVertex(IVertex vertex)
        {
            (vertex as ConsoleVertex).OnCostChanged += ChangeVertexValue;
            (vertex as ConsoleVertex).OnDestinationChosen += ChooseExtremeVertices;
            (vertex as ConsoleVertex).OnRoleChanged += ReversePolarity;
        }
    }
}
