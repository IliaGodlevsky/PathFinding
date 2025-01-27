using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.Messages.ViewModel
{
    internal sealed record class IsVertexInRangeRequest(GraphVertexModel Vertex)
    {
        public bool IsInRange { get; set; }
    }
}
