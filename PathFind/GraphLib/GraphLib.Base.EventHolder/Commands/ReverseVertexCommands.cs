using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EventHolder.Commands
{
    internal sealed class ReverseVertexCommands : IVerticesCommands
    {
        public ReverseVertexCommands()
        {
            commands = new IVertexCommand[]
            {
                new SetVertexAsObstacleCommand(),
                new SetVertexAsRegularCommand()
            };
        }

        public void Execute(IVertex vertex)
        {
            if (!vertex.IsNull())
            {
                commands.Execute(vertex);
            }
        }

        public void Reset() { }

        private readonly IVertexCommand[] commands;
    }
}
