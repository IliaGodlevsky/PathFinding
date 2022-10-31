using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Realizations
{
    public sealed class CompositeGraphEvents<TVertex> : IGraphEvents<TVertex>
        where TVertex : IVertex
    {
        private readonly IEnumerable<IGraphEvents<TVertex>> events;

        public CompositeGraphEvents(params IGraphEvents<TVertex>[] events)
        {
            this.events = events;
        }

        public void Subscribe(IGraph<TVertex> graph)
        {
            events.ForEach(@event => @event.Subscribe(graph));
        }

        public void Unsubscribe(IGraph<TVertex> graph)
        {
            events.ForEach(@event => @event.Unsubscribe(graph));
        }
    }
}
