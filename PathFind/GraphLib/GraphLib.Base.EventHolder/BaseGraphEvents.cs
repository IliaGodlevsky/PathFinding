using Commands.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Base.EventHolder.Commands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;

namespace GraphLib.Base.EventHolder
{
    public abstract class BaseGraphEvents : IGraphEvents
    {
        private readonly IExecutable<IVertex> commands;
        protected readonly IVertexCostFactory costFactory;

        protected BaseGraphEvents(IVertexCostFactory costFactory)
        {
            this.costFactory = costFactory;
            commands = new ReverseVertexCommands();
        }

        protected virtual void ChangeVertexCost(object sender, EventArgs e)
        {
            if (sender is IVertex vertex && !vertex.IsObstacle)
            {
                int delta = GetWheelDelta(e);
                int newCost = vertex.Cost.CurrentCost + delta;
                vertex.Cost = costFactory.CreateCost(newCost);
            }
        }

        protected virtual void Reverse(object sender, EventArgs e)
        {
            commands.Execute(sender.AsVertex());
        }

        public virtual void Unsubscribe(IGraph graph)
        {
            graph.ForEach(UnsubscribeFromEvents);
        }

        public virtual void Subscribe(IGraph graph)
        {
            graph.ForEach(SubscribeToEvents);
        }

        protected abstract void UnsubscribeFromEvents(IVertex vertex);

        protected abstract void SubscribeToEvents(IVertex vertex);

        protected abstract int GetWheelDelta(EventArgs e);
    }
}