using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.VertexActions;

namespace Pathfinding.App.Console.Interface
{
    internal interface INeighbourhoodCommand
    {
        void Execute(ActiveVertex active, Vertex vertex);

        bool CanExecute(ActiveVertex active, Vertex vertex);
    }
}
