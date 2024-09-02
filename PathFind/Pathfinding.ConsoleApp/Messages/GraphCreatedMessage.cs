using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Service.Interface.Models.Read;

namespace Pathfinding.ConsoleApp.Messages
{
    internal sealed class GraphCreatedMessage
    {
        public GraphModel<VertexViewModel> Model { get; }

        public GraphCreatedMessage(GraphModel<VertexViewModel> model)
        {
            Model = model;
        }
    }
}
