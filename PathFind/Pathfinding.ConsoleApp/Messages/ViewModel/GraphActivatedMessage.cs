using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Interface;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed record class GraphActivatedMessage(int GraphId, IGraph<GraphVertexModel> Graph);
}
