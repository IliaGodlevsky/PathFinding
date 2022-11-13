using Pathfinding.App.Console.EventArguments;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Subscriptions;

using static Pathfinding.GraphLib.Core.Realizations.VertexCost;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ConsoleVertexChangeCostSubscription : ChangeVertexCostSubscription<Vertex>, IRequireIntInput
    {
        public IInput<int> IntInput { get; set; }

        public ConsoleVertexChangeCostSubscription(IVertexCostFactory costFactory)
            : base(costFactory)
        {

        }

        private void ChangeVertexCost(object sender, VertexEventArgs e)
        {
            var vertex = e.Current;
            if (!vertex.IsObstacle)
            {
                var cost = IntInput.Input(MessagesTexts.VertexCostInputMsg, CostRange);
                ChangeVertexCost(vertex, cost);
            }
        }

        protected override void SubscribeVertex(Vertex vertex)
        {
            vertex.CostChanged += ChangeVertexCost;
        }

        protected override void UnsubscribeVertex(Vertex vertex)
        {
            vertex.CostChanged -= ChangeVertexCost;
        }
    }
}
