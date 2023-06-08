using Pathfinding.App.Console.Dto;

namespace Pathfinding.App.Console.Repositories
{
    internal sealed class GraphMemoryRepository : MemoryRepository<GraphDto>
    {
        protected override GraphDto Update(GraphDto entity, GraphDto value)
        {
            value.Vertices = entity.Vertices;
            return value;
        }
    }
}
