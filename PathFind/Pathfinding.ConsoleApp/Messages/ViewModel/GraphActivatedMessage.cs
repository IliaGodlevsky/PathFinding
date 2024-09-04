using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Interface;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class GraphActivatedMessage
    {
        public int GraphId { get; }

        public IGraph<VertexModel> Graph { get; }

        public GraphActivatedMessage(int graphId,
            IGraph<VertexModel> graph)
        {
            GraphId = graphId;
            Graph = graph;
        }
    }
}
