using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Collections;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Visualization.Subscriptions.Commands
{
    internal sealed class ReverseVertexCommands<TVertex> : IExecutable<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly ReadOnlyList<IVisualizationCommand<TVertex>> commands;

        public ReverseVertexCommands()
        {
            commands = GetCommands().ToReadOnly();
        }

        public void Execute(TVertex vertex)
        {
            commands.Execute(vertex);
        }

        private IEnumerable<IVisualizationCommand<TVertex>> GetCommands()
        {
            yield return new SetVertexAsObstacleCommand<TVertex>();
            yield return new SetVertexAsRegularCommand<TVertex>();
        }
    }
}