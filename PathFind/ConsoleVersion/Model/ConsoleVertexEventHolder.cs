using ConsoleVersion.InputClass;
using System;
using GraphLib.EventHolder;
using GraphLib.Vertex.Interface;
using GraphLib.Vertex.Cost;
using Common.ValueRanges;

namespace ConsoleVersion.Model
{
    internal class ConsoleVertexEventHolder : BaseVertexEventHolder
    {
        public override void ChangeVertexCost(object sender, EventArgs e)
        {
            var vertex = sender as ConsoleVertex;

            if (!vertex.IsObstacle)
            {
                var cost = Input.InputNumber(
                    ConsoleVersionResources.VertexCostInputMsg,
                      Range.VertexCostRange.UpperRange,
                      Range.VertexCostRange.LowerRange);

                vertex.Cost = new VertexCost(cost);
            }
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return 0;
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            (vertex as ConsoleVertex).OnCostChanged += ChangeVertexCost;
            (vertex as ConsoleVertex).OnExtremeVertexChosen += ChooseExtremeVertices;
            (vertex as ConsoleVertex).OnReverse += Reverse;
        }
    }
}
