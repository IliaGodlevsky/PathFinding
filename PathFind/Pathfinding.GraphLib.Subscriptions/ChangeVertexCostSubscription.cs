using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Extensions;

namespace Pathfinding.GraphLib.Subscriptions
{
    public abstract class ChangeVertexCostSubscription<TVertex> : IGraphSubscription<TVertex>
        where TVertex : IVertex
    {
        private readonly IVertexCostFactory costFactory;

        protected ChangeVertexCostSubscription(IVertexCostFactory costFactory)
        {
            this.costFactory = costFactory;
        }

        public void Subscribe(IGraph<TVertex> graph)
        {
            graph.ForEach(SubscribeVertex);
        }

        public void Unsubscribe(IGraph<TVertex> graph)
        {
            graph.ForEach(UnsubscribeVertex);
        }

        protected void ChangeVertexCost(TVertex vertex, int cost)
        {
            vertex.Cost = costFactory.CreateCost(cost);
        }

        protected abstract void SubscribeVertex(TVertex vertex);

        protected abstract void UnsubscribeVertex(TVertex vertex);
    }
}
