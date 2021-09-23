using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;

using static ConsoleVersion.Resource.Resources;
using static GraphLib.Base.BaseVertexCost;

namespace ConsoleVersion.Model
{
    internal sealed class VertexEventHolder : BaseVertexEventHolder, IVertexEventHolder, IRequireInt32Input
    {
        public VertexEventHolder(IVertexCostFactory costFactory)
            : base(costFactory)
        {

        }

        public IValueInput<int> Int32Input { get; set; }

        public override void ChangeVertexCost(object sender, EventArgs e)
        {
            if (sender is Vertex vertex)
            {
                if (!vertex.IsObstacle)
                {
                    var cost = Int32Input.InputValue(VertexCostInputMsg, CostRange);
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
                vert.OnVertexCostChanged += ChangeVertexCost;
                vert.OnVertexReversed += Reverse;
            }
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.OnVertexCostChanged -= ChangeVertexCost;
                vert.OnVertexReversed -= Reverse;
            }
        }
    }
}
