using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Visualization.Subscriptions.Commands;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using Shared.Extensions;

namespace Pathfinding.GraphLib.Visualization.Subscriptions
{
    public abstract class ReverseVertexSubscription<TVertex> : IGraphSubscription<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly IExecutable<TVertex> commands;

        protected ReverseVertexSubscription()
        {
            commands = new ReverseVertexCommands<TVertex>();
        }

        protected virtual void Reverse(TVertex vertex)
        {
            commands.Execute(vertex);
        }

        public void Unsubscribe(IGraph<TVertex> graph)
        {
            graph.ForEach(UnsubscribeVertex);
        }

        public void Subscribe(IGraph<TVertex> graph)
        {
            graph.ForEach(SubscribeVertex);
        }

        protected abstract void UnsubscribeVertex(TVertex vertex);

        protected abstract void SubscribeVertex(TVertex vertex);
    }
}