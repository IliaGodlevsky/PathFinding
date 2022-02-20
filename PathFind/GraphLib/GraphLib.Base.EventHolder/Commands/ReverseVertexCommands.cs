using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;

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
            commands.Execute(vertex ?? NullVertex.Instance);
        }

        public void Undo() { }

        private readonly IVertexCommand[] commands;
    }
}
