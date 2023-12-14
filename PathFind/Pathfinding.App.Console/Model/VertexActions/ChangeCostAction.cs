using Pathfinding.App.Console.Interface;
using Shared.Primitives.Extensions;
using System;

namespace Pathfinding.App.Console.Model.VertexActions
{
    internal abstract class ChangeCostAction : IVertexAction
    {
        protected abstract int Increment { get; }

        public void Invoke(Vertex vertex)
        {
            var range = vertex.Cost.CostRange;
            int newCost = vertex.Cost.CurrentCost + Increment;
            newCost = range.ReturnInRange(newCost);
            vertex.Cost.CurrentCost = newCost;
        }
    }
}
