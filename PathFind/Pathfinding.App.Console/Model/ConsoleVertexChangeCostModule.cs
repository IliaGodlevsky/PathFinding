using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Primitives.ValueRange;
using static Pathfinding.GraphLib.Core.Realizations.VertexCost;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ConsoleVertexChangeCostModule : IRequireIntInput
    {
        public IInput<int> IntInput { get; set; }

        public void ChangeVertexCost(Vertex vertex)
        {
            using (Cursor.CleanUpAfter())
            {
                if (!vertex.IsObstacle)
                {
                    var range = vertex.Cost.CostRange;
                    var cost = IntInput.Input(MessagesTexts.VertexCostInputMsg, range);
                    vertex.Cost = vertex.Cost.SetCost(cost);
                    vertex.Display();
                }
            }
        }
    }
}
