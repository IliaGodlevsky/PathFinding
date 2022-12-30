using Pathfinding.App.Console.Interface;
using Shared.Primitives.Extensions;

namespace Pathfinding.App.Console.Model.VertexActions
{
    internal abstract class ChangeCostAction : IVertexAction
    {
        protected abstract int Increment { get; }

        public void Do(Vertex vertex)
        {
            var range = vertex.Cost.CostRange;
            int newCost = vertex.Cost.CurrentCost + Increment;
            newCost = range.ReturnInRange(newCost);
            vertex.Cost = vertex.Cost.SetCost(newCost);
            vertex.Display();
        }
    }
}
