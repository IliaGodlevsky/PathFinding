using Pathfinding.App.Console.Interface;
using Shared.Extensions;

namespace Pathfinding.App.Console.Model.VertexActions.NeighbourhoodActions
{
    internal sealed class ActivateVertexCommand : INeighbourhoodCommand
    {
        public bool CanExecute(ActiveVertex active, Vertex vertex)
        {
            return active.Current is null;
        }

        public void Execute(ActiveVertex active, Vertex vertex)
        {
            active.Current = vertex;
            var factory = new MooreNeighborhoodFactory();
            var availaible = factory.CreateNeighborhood(vertex.Position);
            active.Availiable.AddRange(availaible);

            foreach (Vertex neighbour in vertex.Neighbours)
            {
                if (!neighbour.IsObstacle && !neighbour.IsVisualizedAsRange())
                {
                    neighbour.VisualizeAsVisited();
                }
            }
        }
    }
}
