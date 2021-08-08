using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;
using System.Threading.Tasks;

namespace GraphLib.Base
{
    public abstract class BaseVertexEventHolder : IVertexEventHolder
    {
        protected BaseVertexEventHolder(IVertexCostFactory costFactory)
        {
            this.costFactory = costFactory;
        }

        protected readonly IVertexCostFactory costFactory;

        public virtual void ChangeVertexCost(object sender, EventArgs e)
        {
            if (sender is IVertex vertex && !vertex.IsObstacle)
            {
                int delta = GetWheelDelta(e) > 0 ? 1 : -1;
                int newCost = vertex.Cost.CurrentCost + delta;
                vertex.Cost = costFactory.CreateCost(newCost);
            }
        }

        public virtual void Reverse(object sender, EventArgs e)
        {
            if (sender is IVertex vertex && vertex is IMarkable markable)
            {
                if (vertex.IsObstacle)
                {
                    vertex.IsObstacle = false;
                    markable.MarkAsRegular();
                }
                else
                {
                    vertex.IsObstacle = true;
                    markable.MarkAsObstacle();
                }
            }
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
    }
}