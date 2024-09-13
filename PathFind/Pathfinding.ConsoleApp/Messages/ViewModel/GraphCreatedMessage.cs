using Pathfinding.ConsoleApp.Model;
using Pathfinding.Service.Interface.Models.Read;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class GraphCreatedMessage
    {
        public GraphModel<VertexModel>[] Models { get; }

        public GraphCreatedMessage(GraphModel<VertexModel>[] models)
        {
            Models = models;
        }
    }
}
