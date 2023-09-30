using Pathfinding.App.Console.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Model.VertexActions.NeighbourhoodActions
{
    internal sealed class NeighbourhoodAction : IVertexAction
    {
        private IEnumerable<INeighbourhoodCommand> Commands { get; }

        private ActiveVertex Active { get; }

        public NeighbourhoodAction(ActiveVertex active)
        {
            Active = active;
            Commands = new INeighbourhoodCommand[]
            {
                new ActivateVertexCommand(),
                new IncludeVertexCommand(),
                new ExcludeVertexCommand()
            };
        }

        public void Do(Vertex vertex)
        {
            using (Cursor.HideCursor())
            {
                Commands
                    .FirstOrDefault(cmd => cmd.CanExecute(Active, vertex))
                    ?.Execute(Active, vertex);
            }
        }
    }
}
