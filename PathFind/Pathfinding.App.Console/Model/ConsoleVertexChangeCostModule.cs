using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Subscriptions;

using static Pathfinding.GraphLib.Core.Realizations.VertexCost;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ConsoleVertexChangeCostModule : ChangeVertexCostModule<Vertex>, IRequireIntInput
    {
        public IInput<int> IntInput { get; set; }

        public ConsoleVertexChangeCostModule(IVertexCostFactory costFactory)
            : base(costFactory)
        {

        }

        public void ChangeVertexCost(Vertex vertex)
        {
            if (!vertex.IsObstacle)
            {
                var cost = IntInput.Input(MessagesTexts.VertexCostInputMsg, CostRange);
                ChangeVertexCost(vertex, cost);
            }
        }
    }
}
