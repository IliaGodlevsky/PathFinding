using Pathfinding.ConsoleApp.Model;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class IsVertexInRangeRequest
    {
        public VertexModel Vertex { get; }

        public bool IsInRange { get; set; }

        public IsVertexInRangeRequest(VertexModel vertex)
        {
            Vertex = vertex;
        }
    }
}
