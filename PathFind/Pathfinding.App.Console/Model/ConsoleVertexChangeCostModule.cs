using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Factory.Interface;

using static Pathfinding.GraphLib.Core.Realizations.VertexCost;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ConsoleVertexChangeCostModule : IRequireIntInput
    {
        private readonly IVertexCostFactory costFactory;

        public IInput<int> IntInput { get; set; }

        public ConsoleVertexChangeCostModule(IVertexCostFactory costFactory)
        {
            this.costFactory = costFactory;
        }

        public void ChangeVertexCost(Vertex vertex)
        {
            if (!vertex.IsObstacle)
            {
                var cost = IntInput.Input(MessagesTexts.VertexCostInputMsg, CostRange);
                vertex.Cost = costFactory.CreateCost(cost);
            }
        }
    }
}
