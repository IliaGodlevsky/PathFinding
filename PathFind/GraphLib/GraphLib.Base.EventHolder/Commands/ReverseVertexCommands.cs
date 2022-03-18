using Commands.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;

namespace GraphLib.Base.EventHolder.Commands
{
    internal sealed class ReverseVertexCommands : IVerticesCommands
    {
        private readonly IVertexCommand[] commands;

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
            commands.ExecuteFirst(vertex ?? NullVertex.Instance);
        }

        public void Undo() { }        
    }
}