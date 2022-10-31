using Commands.Extensions;
using Commands.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Base.EventHolder.Commands
{
    internal sealed class ReverseVertexCommands<TVertex> : IExecutable<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly IEnumerable<IVertexCommand<TVertex>> commands;

        public ReverseVertexCommands()
        {
            commands = GetCommands().ToReadOnly();
        }

        public void Execute(TVertex vertex)
        {
            commands.ExecuteFirst(vertex);
        }

        private IEnumerable<IVertexCommand<TVertex>> GetCommands()
        {
            yield return new SetVertexAsObstacleCommand<TVertex>();
            yield return new SetVertexAsRegularCommand<TVertex>();
        }
    }
}