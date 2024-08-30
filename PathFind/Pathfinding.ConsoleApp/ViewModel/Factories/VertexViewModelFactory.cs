using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.ConsoleApp.ViewModel.Factories
{
    internal sealed class VertexViewModelFactory : IVertexFactory<VertexViewModel>
    {
        public VertexViewModel CreateVertex(Coordinate coordinate)
        {
            return new VertexViewModel(coordinate);
        }
    }
}
