using Pathfinding.Domain.Interface;

namespace Pathfinding.Service.Interface.Requests.Update
{
    public class UpdateVerticesRequest<T>
        where T : IVertex
    {
        public int GraphId { get; }

        public List<T> Vertices { get; } = new List<T>();

        public UpdateVerticesRequest(int graphId, List<T> vertices)
        {
            GraphId = graphId;
            Vertices = vertices;
        }
    }
}
