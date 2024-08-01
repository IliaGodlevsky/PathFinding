using Pathfinding.Domain.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Domain.Interface.Repositories
{
    public interface IAlgorithmsRepository
    {
        Task<IEnumerable<Algorithm>> GetAllAsync(CancellationToken token = default);
    }
}
