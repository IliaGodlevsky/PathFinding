using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using GraphLib.Base.EventHolder;
using GraphLib.Interfaces.Factories;
using System;

using static GraphLib.Base.BaseVertexCost;

namespace ConsoleVersion.Model
{
    internal sealed class GraphEvents : BaseGraphEvents<Vertex>, IRequireIntInput
    {
        public IInput<int> IntInput { get; set; }

        public GraphEvents(IVertexCostFactory costFactory)
            : base(costFactory)
        {

        }

        protected override void ChangeVertexCost(object sender, EventArgs e)
        {
            if (sender is Vertex vertex && !vertex.IsObstacle)
            {
                var cost = IntInput.Input(MessagesTexts.VertexCostInputMsg, CostRange);
                vertex.Cost = costFactory.CreateCost(cost);
            }
        }

        protected override int GetWheelDelta(EventArgs e)
        {
            return 0;
        }

        protected override void SubscribeToEvents(Vertex vertex)
        {
            vertex.VertexCostChanged += ChangeVertexCost;
            vertex.VertexReversed += Reverse;
        }

        protected override void UnsubscribeFromEvents(Vertex vertex)
        {
            vertex.VertexCostChanged -= ChangeVertexCost;
            vertex.VertexReversed -= Reverse;
        }
    }
}
