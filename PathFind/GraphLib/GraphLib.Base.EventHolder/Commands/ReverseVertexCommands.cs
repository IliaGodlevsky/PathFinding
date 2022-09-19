using Commands.Extensions;
using Commands.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using System.Collections.Generic;

namespace GraphLib.Base.EventHolder.Commands
{
    internal sealed class ReverseVertexCommands : IExecutable<IVertex>
    {
        private readonly IEnumerable<IVertexCommand> commands;

        public ReverseVertexCommands()
        {
            commands = GetCommands().ToReadOnly();
        }

        public void Execute(IVertex vertex)
        {
            commands.ExecuteFirst(vertex ?? NullVertex.Interface);
        }

        private IEnumerable<IVertexCommand> GetCommands()
        {
            yield return new SetVertexAsObstacleCommand();
            yield return new SetVertexAsRegularCommand();
        }
    }
}