using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Service.Interface.Extensions
{
    public static class RequestServiceExtensions
    {
        public static async Task<PathfindingHistoryModel<T>> CreatePathfindingHistoryAsync<T>(this IRequestService<T> service,
            CreatePathfindingHistoryRequest<T> request, CancellationToken token = default)
            where T : IVertex, IEntity<int>
        {
            var result = await service.CreatePathfindingHistoriesAsync(request.Enumerate(), token);
            return result.Single();
        }

        public static async Task<PathfindingHistoryModel<T>> CreatePathfindingHistoryAsync<T>(this IRequestService<T> service,
            PathfindingHistorySerializationModel model,
            CancellationToken token = default)
            where T : IVertex, IEntity<int>
        {
            var result = await service.CreatePathfindingHistoriesAsync(model.Enumerate(), token);
            return result.Single();
        }
    }
}
