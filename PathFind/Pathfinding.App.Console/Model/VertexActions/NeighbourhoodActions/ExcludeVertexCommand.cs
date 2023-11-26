using Pathfinding.App.Console.DataAccess.Repo;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Settings;

namespace Pathfinding.App.Console.Model.VertexActions.NeighbourhoodActions
{
    internal sealed class ExcludeVertexCommand : INeighbourhoodCommand
    {
        private readonly IDbContextService service;

        public ExcludeVertexCommand(IDbContextService service)
        {
            this.service = service;
        }

        public bool CanExecute(ActiveVertex active, Vertex vertex)
        {
            return active.Availiable.Contains(vertex.Position)
                && active.Current?.Neighbours.Count > 1
                && active.Current?.Neighbours.Contains(vertex) == true;
        }

        public void Execute(ActiveVertex active, Vertex vertex)
        {
            active.Current.Neighbours.Remove(vertex);
            service.DeleteNeighbour(active.Current, vertex);
            if (vertex.Color == Colours.Default.NeighbourhoodColor)
            {
                vertex.VisualizeAsRegular();
            }
        }
    }
}
