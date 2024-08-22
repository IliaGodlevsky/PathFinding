using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Service.Interface.Requests.Delete;
using Pathfinding.Service.Interface.Requests.Update;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Service.Interface
{
    public interface IRequestService<T>
        where T : IVertex, IEntity<int>
    {
        Task<int> ReadRunCountAsync(int graphId, CancellationToken token = default);

        Task<int> ReadGraphCountAsync(CancellationToken token = default);

        Task<GraphModel<T>> ReadGraphAsync(int graphId, CancellationToken token = default);

        Task<IReadOnlyCollection<int>> ReadGraphIdsAsync(CancellationToken token = default);

        Task<IReadOnlyCollection<RunStatisticsModel>> ReadRunStatisticsAsync(int graphId,
            CancellationToken token = default);

        Task<RunVisualizationModel> ReadRunInfoAsync(int runId, CancellationToken token = default);

        Task<IReadOnlyCollection<GraphInformationModel>> ReadAllGraphInfoAsync(CancellationToken token = default);

        Task<PathfindingHistoryModel<T>> ReadPathfindingHistoryAsync(int graphId,
            CancellationToken token = default);

        Task<PathfindingRangeModel> ReadRangeAsync(int graphId,
            CancellationToken token = default);

        Task<bool> UpdateObstaclesCountAsync(UpdateGraphInfoRequest request,
            CancellationToken token = default);

        Task<IReadOnlyCollection<PathfindingHistoryModel<T>>> CreatePathfindingHistoriesAsync(IEnumerable<CreatePathfindingHistoryRequest<T>> request,
            CancellationToken token = default);

        Task<IReadOnlyCollection<PathfindingHistoryModel<T>>> CreatePathfindingHistoriesAsync(IEnumerable<PathfindingHistorySerializationModel> request,
            CancellationToken token = default);

        Task<PathfindingHistorySerializationModel> ReadSerializationHistoryAsync(int graphId,
            CancellationToken token = default);

        Task<GraphSerializationModel> ReadSerializationGraphAsync(int graphId,
            CancellationToken token = default);

        Task<GraphModel<T>> CreateGraphAsync(GraphSerializationModel graph,
            CancellationToken token = default);

        Task<GraphModel<T>> CreateGraphAsync(CreateGraphRequest<T> graph,
            CancellationToken token = default);

        Task<IReadOnlyCollection<GraphModel<T>>> CreateGraphsAsync(IEnumerable<GraphSerializationModel> request,
            CancellationToken token = default);

        Task<IReadOnlyCollection<AlgorithmRunHistoryModel>> CreateRunHistoriesAsync(IEnumerable<CreateAlgorithmRunHistoryRequest> histories,
            CancellationToken token = default);

        Task<bool> CreateNeighborsAsync(CreateNeighborsRequest<T> request,
            CancellationToken token = default);

        Task<bool> UpdateVerticesAsync(UpdateVerticesRequest<T> request,
            CancellationToken token = default);

        Task<bool> CreateRangeAsync(CreatePathfindingRangeRequest<T> request,
            CancellationToken token = default);

        Task<bool> RemoveNeighborsAsync(IReadOnlyDictionary<T, IReadOnlyCollection<T>> request,
            CancellationToken token = default);

        Task<bool> UpdateRangeAsync(IEnumerable<(int Order, T Vertex)> request,
            CancellationToken token = default);

        Task<bool> DeleteRangeAsync(IEnumerable<T> request,
            CancellationToken token = default);

        Task<bool> DeleteRangeAsync(int graphId,
            CancellationToken token = default);

        Task<bool> DeleteGraphAsync(int graphId,
            CancellationToken token = default);

        Task<bool> DeleteGraphsAsync(IEnumerable<int> ids,
            CancellationToken token = default);
    }
}
