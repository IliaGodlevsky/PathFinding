using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.ValueInput;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;

using static GraphLib.Base.BaseVertexCost;

namespace ConsoleVersion.Model
{
    internal sealed class VertexEventHolder : BaseVertexEventHolder, IVertexEventHolder, IRequireInt32Input
    {
        public VertexEventHolder(IVertexCostFactory costFactory)
            : base(costFactory)
        {

        }

        public ConsoleValueInput<int> Int32Input { get; set; }

        public override void ChangeVertexCost(object sender, EventArgs e)
        {
            if (sender is Vertex vertex && !vertex.IsObstacle)
            {
                var cost = Int32Input.InputValue(MessagesTexts.VertexCostInputMsg, CostRange);
                vertex.Cost = costFactory.CreateCost(cost);
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
                vert.VertexCostChanged += ChangeVertexCost;
                vert.VertexReversed += Reverse;
            }
        }

        protected override void UnsubscribeFromEvents(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.VertexCostChanged -= ChangeVertexCost;
                vert.VertexReversed -= Reverse;
            }
        }
    }
}
