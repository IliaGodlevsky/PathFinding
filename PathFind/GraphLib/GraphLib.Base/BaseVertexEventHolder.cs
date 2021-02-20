using Common;
using Common.Interfaces;
using GraphLib.Extensions;
using GraphLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Base
{
    public abstract class BaseVertexEventHolder : IVertexEventHolder
    {
        public BaseVertexEventHolder()
        {
            commands = new Command<IVertex>[]
            {
                new Command<IVertex>(vertex => vertex.IsObstacle,  MakeVertex),
                new Command<IVertex>(vertex => !vertex.IsObstacle, MakeObstacle)
            };
        }

        public IVertexCostFactory CostFactory { get; set; }

        public IGraph Graph { get; set; }

        public virtual void ChangeVertexCost(object sender, EventArgs e)
        {
            if (sender is IVertex vertex)
            {
                if (!vertex.IsObstacle)
                {
                    int delta = GetWheelDelta(e) > 0 ? 1 : -1;
                    int newCost = vertex.Cost.CurrentCost + delta;
                    vertex.Cost = CostFactory.CreateCost(newCost);
                }
            }
        }

        public virtual void Reverse(object sender, EventArgs e)
        {
            if (sender is IVertex vertex)
            {
                Command<IVertex>.ExecuteFirstExecutable(commands, vertex);
            }
        }

        public void UnsubscribeVertices()
        {
            Graph?.Vertices.AsParallel().ForAll(UnsubscribeFromEvents);
        }

        public void SubscribeVertices()
        {
            Graph?.Vertices.AsParallel().ForAll(SubscribeToEvents);
        }

        protected abstract void UnsubscribeFromEvents(IVertex vertex);

        protected abstract void SubscribeToEvents(IVertex vertex);

        protected abstract int GetWheelDelta(EventArgs e);

        private void MakeObstacle(IVertex vertex)
        {
            vertex.Isolate();
            vertex.IsObstacle = true;
            vertex.SetToDefault();
            if (vertex is IMarkableVertex vert)
            {
                vert.MarkAsObstacle();
            }
        }

        private void MakeVertex(IVertex vertex)
        {
            vertex.IsObstacle = false;
            if (vertex is IMarkableVertex vert)
            {
                vert.MarkAsSimpleVertex();
            }
            vertex.SetNeighbours(Graph);
            vertex.ConnectWithNeighbours();
        }

        private readonly IEnumerable<Command<IVertex>> commands;
    }
}
