using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Visualization.Subscriptions.Commands;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;

namespace Pathfinding.GraphLib.Visualization.Subscriptions
{
    public abstract class ReverseVertexModule<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly IExecutable<TVertex> commands;

        protected ReverseVertexModule()
        {
            commands = new ReverseVertexCommands<TVertex>();
        }

        protected virtual void Reverse(TVertex vertex)
        {
            commands.Execute(vertex);
        }
    }
}