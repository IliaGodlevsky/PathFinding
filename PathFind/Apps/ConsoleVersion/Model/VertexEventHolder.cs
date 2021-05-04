using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;
using static ConsoleVersion.InputClass.Input;
using static ConsoleVersion.Resource.Resources;
using static GraphLib.Base.BaseVertexCost;

namespace ConsoleVersion.Model
{
    internal sealed class VertexEventHolder : BaseVertexEventHolder
    {
        public VertexEventHolder(IVertexCostFactory costFactory)
            : base(costFactory)
        {

        }

        public override void ChangeVertexCost(object sender, EventArgs e)
        {
            if (sender is Vertex vertex)
            {
                if (!vertex.IsObstacle)
                {
                    var cost = InputNumber(VertexCostInputMsg, CostRange);
                    vertex.Cost = costFactory.CreateCost(cost);
                }
            }
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return 0;
        }

        protected override void SubscribeToEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.OnCostChanged += ChangeVertexCost;
                vert.OnReverse += Reverse;
            }
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.OnCostChanged -= ChangeVertexCost;
                vert.OnReverse -= Reverse;
            }
        }
    }
}
