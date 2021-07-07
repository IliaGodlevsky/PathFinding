using Conditional;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System;
using System.Collections.Generic;

namespace GraphLib.Base
{
    public abstract class BaseVertexEventHolder : IVertexEventHolder
    {
        protected BaseVertexEventHolder(IVertexCostFactory costFactory)
        {
            this.costFactory = costFactory;
            reverseActionDictionary = new Dictionary<bool, Action<IMarkable>>()
            {
                { true, vertex => vertex.MarkAsObstacle() },
                { false, vertex => vertex.MarkAsRegular() }
            };

            conditional = new Conditional<IVertex>(MakeVertex, v => v.IsObstacle)
                   .PerformIf(MakeObstacle);
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
            conditional.PerformFirstSuitable(sender as IVertex, param => param != null);
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

        private void MakeObstacle(IVertex vertex)
        {
            if (vertex is IMarkable markable)
            {
                reverseActionDictionary[vertex.IsObstacle = true](markable);
            }
        }

        private void MakeVertex(IVertex vertex)
        {
            if (vertex is IMarkable markable)
            {
                reverseActionDictionary[vertex.IsObstacle = false](markable);
            }
        }

        private readonly Dictionary<bool, Action<IMarkable>> reverseActionDictionary;
        private readonly Conditional<IVertex> conditional;
    }
}
