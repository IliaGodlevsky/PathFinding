using AutoMapper;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Business.Extensions
{
    public static class AutomapperExtensions
    {
        public static async Task<T> MapAsync<T>(this IMapper mapper, object source,
            CancellationToken token = default)
        {
            return await Task.Run(()=> mapper.Map<T>(source), token);
        }
    }
}
