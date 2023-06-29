using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Settings;
using Pathfinding.GraphLib.Factory.Interface;

namespace Pathfinding.App.Console.Model.VertexActions.NeighbourhoodActions
{
    internal sealed class IncludeVertexCommand : INeighbourhoodCommand
    {
        public bool CanExecute(ActiveVertex active, Vertex vertex)
        {
            return active.Current?.Neighbours.Contains(vertex) == false
                && active.Availiable.Contains(vertex.Position);
        }

        public void Execute(ActiveVertex active, Vertex vertex)
        {
            active.Current.Neighbours.Add(vertex);
            if (!vertex.IsObstacle && !vertex.IsVisualizedAsRange())
            {
                vertex.Color = Colours.Default.NeighbourhoodColor;
            }
        }
    }
}
