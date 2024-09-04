using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Service.Interface.Models.Read;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class GraphCreatedMessage
    {
        public GraphModel<VertexModel> Model { get; }

        public GraphCreatedMessage(GraphModel<VertexModel> model)
        {
            Model = model;
        }
    }
}
