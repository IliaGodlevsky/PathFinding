using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Realizations
{
    public sealed class CompositeGraphEvents : IGraphEvents
    {
        private readonly IEnumerable<IGraphEvents> events;

        public CompositeGraphEvents(params IGraphEvents[] events)
        {
            this.events = events;
        }

        public void Subscribe(IGraph graph)
        {
            events.ForEach(@event => @event.Subscribe(graph));
        }

        public void Unsubscribe(IGraph graph)
        {
            events.ForEach(@event => @event.Unsubscribe(graph));
        }
    }
}
