using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;

namespace GraphLib.Realizations
{
    public sealed class CompositeGraphEvents : IGraphEvents
    {
        private readonly IGraphEvents[] events;

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
