using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Settings;

namespace Pathfinding.App.Console.Model.VertexActions.NeighbourhoodActions
{
    internal sealed class ExcludeVertexCommand : INeighbourhoodCommand
    {
        public bool CanExecute(ActiveVertex active, Vertex vertex)
        {
            return active.Availiable.Contains(vertex.Position)
                && active.Current?.Neighbours.Contains(vertex) == true
                && active.Current?.Neighbours.Count > 1;
        }

        public void Execute(ActiveVertex active, Vertex vertex)
        {
            active.Current.Neighbours.Remove(vertex);
            if (vertex.Color == Colours.Default.NeighbourhoodColor)
            {
                vertex.VisualizeAsRegular();
            }
        }
    }
}
