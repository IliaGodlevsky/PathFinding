using Commands.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Base.EventHolder.Commands;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;

namespace GraphLib.Base.EventHolder
{
    public abstract class BaseGraphEvents<TVertex> : IGraphEvents<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly IExecutable<TVertex> commands;
        protected readonly IVertexCostFactory costFactory;

        protected BaseGraphEvents(IVertexCostFactory costFactory)
        {
            this.costFactory = costFactory;
            commands = new ReverseVertexCommands<TVertex>();
        }

        protected virtual void ChangeVertexCost(object sender, EventArgs e)
        {
            if (sender is TVertex vertex && !vertex.IsObstacle)
            {
                int delta = GetWheelDelta(e);
                int newCost = vertex.Cost.CurrentCost + delta;
                vertex.Cost = costFactory.CreateCost(newCost);
            }
        }

        protected virtual void Reverse(object sender, EventArgs e)
        {
            commands.Execute((TVertex)sender);
        }

        public virtual void Unsubscribe(IGraph<TVertex> graph)
        {
            graph.ForEach(UnsubscribeFromEvents);
        }

        public virtual void Subscribe(IGraph<TVertex> graph)
        {
            graph.ForEach(SubscribeToEvents);
        }

        protected abstract void UnsubscribeFromEvents(TVertex vertex);

        protected abstract void SubscribeToEvents(TVertex vertex);

        protected abstract int GetWheelDelta(EventArgs e);
    }
}