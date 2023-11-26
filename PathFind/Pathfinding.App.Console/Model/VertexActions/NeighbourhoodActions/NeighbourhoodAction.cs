using Pathfinding.App.Console.DataAccess.Repo;
using Pathfinding.App.Console.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Model.VertexActions.NeighbourhoodActions
{
    internal sealed class NeighbourhoodAction : IVertexAction
    {
        private IEnumerable<INeighbourhoodCommand> Commands { get; }

        private ActiveVertex Active { get; }

        public NeighbourhoodAction(ActiveVertex active, IDbContextService service)
        {
            Active = active;
            Commands = new INeighbourhoodCommand[]
            {
                new ActivateVertexCommand(),
                new IncludeVertexCommand(service),
                new ExcludeVertexCommand(service)
            };
        }

        public void Invoke(Vertex vertex)
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
