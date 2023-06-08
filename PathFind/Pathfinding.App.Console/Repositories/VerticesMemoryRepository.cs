using Pathfinding.App.Console.Dto;

namespace Pathfinding.App.Console.Repositories
{
    internal class VerticesMemoryRepository : MemoryRepository<VerticesDto>
    {
        protected override VerticesDto Update(VerticesDto entity, VerticesDto value)
        {
            value.ReferenceId = entity.ReferenceId;
            value.Coordinates = entity.Coordinates;
            return value;
        }
    }
}
