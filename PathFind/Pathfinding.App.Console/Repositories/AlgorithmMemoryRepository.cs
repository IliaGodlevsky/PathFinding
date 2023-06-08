using Pathfinding.App.Console.Dto;
using Pathfinding.App.Console.Entities;

namespace Pathfinding.App.Console.Repositories
{
    internal sealed class AlgorithmMemoryRepository : MemoryRepository<AlgorithmDto>
    {
        protected override AlgorithmDto Update(AlgorithmDto entity, AlgorithmDto value)
        {
            value.Name = entity.Name;
            value.GraphId = entity.GraphId;
            return value;
        }
    }
}
