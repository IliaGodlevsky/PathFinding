using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Subscriptions
{
    public sealed class GraphSubscriptions<TVertex> : IGraphSubscription<TVertex>
        where TVertex : IVertex
    {
        private readonly IEnumerable<IGraphSubscription<TVertex>> subscriptions;

        public GraphSubscriptions(params IGraphSubscription<TVertex>[] subscriptions)
        {
            this.subscriptions = subscriptions;
        }

        public void Subscribe(IGraph<TVertex> graph)
        {
            subscriptions.ForEach(subscription => subscription.Subscribe(graph));
        }

        public void Unsubscribe(IGraph<TVertex> graph)
        {
            subscriptions.ForEach(subscription => subscription.Unsubscribe(graph));
        }
    }
}
