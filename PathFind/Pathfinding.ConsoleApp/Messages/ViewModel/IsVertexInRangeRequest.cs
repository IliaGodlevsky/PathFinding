using Pathfinding.ConsoleApp.Model;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed record class IsVertexInRangeRequest(GraphVertexModel Vertex)
    {
        public bool IsInRange { get; set; }
    }
}
