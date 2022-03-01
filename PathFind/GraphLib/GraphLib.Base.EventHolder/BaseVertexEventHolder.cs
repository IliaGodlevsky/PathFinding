using Common.Extensions;
using GraphLib.Base.EventHolder.Commands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.NullRealizations;
using System;

namespace GraphLib.Base.EventHolder
{
    public abstract class BaseVertexEventHolder : IVertexEventHolder
    {
        protected BaseVertexEventHolder(IVertexCostFactory costFactory)
        {
            this.costFactory = costFactory;
            commands = new ReverseVertexCommands();
        }

        public virtual void ChangeVertexCost(object sender, EventArgs e)
        {
            if (sender is IVertex vertex && !vertex.IsObstacle)
            {
                int delta = GetWheelDelta(e);
                int newCost = vertex.Cost.CurrentCost + delta;
                vertex.Cost = costFactory.CreateCost(newCost);
            }
        }

        public virtual void Reverse(object sender, EventArgs e)
        {
            commands.Execute(sender.AsVertex());
        }

        public virtual void UnsubscribeVertices(IGraph graph)
        {
            graph.ForEach(UnsubscribeFromEvents);
        }

        public virtual void SubscribeVertices(IGraph graph)
        {
            graph.ForEach(SubscribeToEvents);
        }

        protected abstract void UnsubscribeFromEvents(IVertex vertex);

        protected abstract void SubscribeToEvents(IVertex vertex);

        protected abstract int GetWheelDelta(EventArgs e);

        private readonly IVerticesCommands commands;
        protected readonly IVertexCostFactory costFactory;
    }
}