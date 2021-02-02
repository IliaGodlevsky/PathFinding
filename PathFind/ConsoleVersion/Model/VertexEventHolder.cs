using Common.ValueRanges;
using ConsoleVersion.InputClass;
using ConsoleVersion.Resource;
using GraphLib.Base;
using GraphLib.Interface;
using GraphLib.Vertex.Cost;
using System;

namespace ConsoleVersion.Model
{
    internal class VertexEventHolder : BaseVertexEventHolder
    {
        public override void ChangeVertexCost(object sender, EventArgs e)
        {
            var vertex = sender as Vertex;

            if (!vertex.IsObstacle)
            {
                var cost = Input.InputNumber(Resources.VertexCostInputMsg, Range.VertexCostRange);

                vertex.Cost = new VertexCost(cost);
            }
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return 0;
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            (vertex as Vertex).OnCostChanged += ChangeVertexCost;
            (vertex as Vertex).OnExtremeVertexChosen += ChooseExtremeVertices;
            (vertex as Vertex).OnReverse += Reverse;
        }
    }
}
