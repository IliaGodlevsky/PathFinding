using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Model.VertexActions.NeighbourhoodActions
{
    internal sealed class NeighbourhoodAction : IVertexAction
    {
        private IEnumerable<INeighbourhoodCommand> Commands { get; }

        private ActiveVertex Active { get; }

        public NeighbourhoodAction(ActiveVertex active, INeighborhoodFactory factory)
        {
            Active = active;
            Commands = new INeighbourhoodCommand[]
            {
                new ActivateVertexCommand(factory),
                new IncludeVertexCommand(),
                new ExcludeVertexCommand()
            };
        }

        public void Do(Vertex vertex)
        {
            using (Cursor.HideCursor())
            {
                var command = Commands.FirstOrDefault(cmd => cmd.CanExecute(Active, vertex));
                if (command != null)
                {
                    command.Execute(Active, vertex);
                }
            }
        }
    }
}
