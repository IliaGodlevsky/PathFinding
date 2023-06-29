using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Settings;
using Pathfinding.GraphLib.Factory.Interface;

namespace Pathfinding.App.Console.Model.VertexActions.NeighbourhoodActions
{
    internal sealed class ActivateVertexCommand : INeighbourhoodCommand
    {
        private readonly INeighborhoodFactory factory;

        public ActivateVertexCommand(INeighborhoodFactory factory)
        {
            this.factory = factory;
        }

        public bool CanExecute(ActiveVertex active, Vertex vertex)
        {
            return active.Current == null;
        }

        public void Execute(ActiveVertex active, Vertex vertex)
        {
            active.Current = vertex;
            var availaible = factory.CreateNeighborhood(vertex.Position);
            foreach (var neighbour in availaible)
            {
                active.Availiable.Add(neighbour);
            }
            foreach (Vertex neighbour in vertex.Neighbours)
            {
                if (!neighbour.IsObstacle && !neighbour.IsVisualizedAsRange())
                {
                    neighbour.Color = Colours.Default.NeighbourhoodColor;
                }
            }
        }
    }
}
