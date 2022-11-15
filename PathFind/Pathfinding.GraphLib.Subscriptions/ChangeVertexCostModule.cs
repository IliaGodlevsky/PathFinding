using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;

namespace Pathfinding.GraphLib.Subscriptions
{
    public abstract class ChangeVertexCostModule<TVertex>
        where TVertex : IVertex
    {
        private readonly IVertexCostFactory costFactory;

        protected ChangeVertexCostModule(IVertexCostFactory costFactory)
        {
            this.costFactory = costFactory;
        }

        protected void ChangeVertexCost(TVertex vertex, int cost)
        {
            vertex.Cost = costFactory.CreateCost(cost);
        }
    }
}
