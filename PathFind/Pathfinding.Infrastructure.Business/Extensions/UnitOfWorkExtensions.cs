using AutoMapper;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Business.Extensions
{
    public static class UnitOfWorkExtensions
    {
        public static async Task AddHistoryAsync(this IUnitOfWork unitOfWork,
            IMapper mapper,
            IEnumerable<CreateAlgorithmRunHistoryRequest> runHistories,
            CancellationToken token = default)
        {
            foreach (var runHistory in runHistories)
            {
                var runEntity = mapper.Map<AlgorithmRun>(runHistory.Run);
                await unitOfWork.RunRepository.CreateAsync(runEntity, token);
                runHistory.Run.Id = runEntity.Id;
                var graphState = mapper.Map<GraphState>(runHistory.GraphState);
                var subAlgorithms = mapper.Map<SubAlgorithm[]>(runHistory.SubAlgorithms);
                var runStatistics = mapper.Map<Statistics>(runHistory.Statistics);
                subAlgorithms.ForEach(x => x.AlgorithmRunId = runEntity.Id);
                runStatistics.AlgorithmRunId = runEntity.Id;
                graphState.AlgorithmRunId = runEntity.Id;
                await unitOfWork.GraphStateRepository.CreateAsync(graphState, token);
                await unitOfWork.SubAlgorithmRepository.CreateAsync(subAlgorithms, token);
                await unitOfWork.StatisticsRepository.CreateAsync(runStatistics, token);
            }
        }

        public static async Task<IReadOnlyCollection<AlgorithmRunHistoryModel>> GetAlgorithmRuns(this IUnitOfWork unitofWork,
            int graphId,
            IMapper mapper,
            CancellationToken token = default)
        {
            var algorithms = await unitofWork.RunRepository.ReadByGraphIdAsync(graphId, token);
            var readModels = mapper.Map<AlgorithmRunModel[]>(algorithms).ToReadOnly();
            var runHistories = new List<AlgorithmRunHistoryModel>();
            foreach (var model in readModels)
            {
                var subAlgorithms = await unitofWork.SubAlgorithmRepository.ReadByAlgorithmRunIdAsync(model.Id, token);
                var graphState = await unitofWork.GraphStateRepository.ReadByRunIdAsync(model.Id, token);
                var runStatistics = await unitofWork.StatisticsRepository.ReadByAlgorithmRunIdAsync(model.Id, token);
                runHistories.Add(new AlgorithmRunHistoryModel()
                {
                    Run = model,
                    SubAlgorithms = mapper.Map<SubAlgorithmModel[]>(subAlgorithms),
                    GraphState = mapper.Map<GraphStateModel>(graphState),
                    Statistics = mapper.Map<RunStatisticsModel>(runStatistics)
                });
            }
            return runHistories.AsReadOnly();
        }

        public static async Task<GraphModel<T>> ReadGraphAsync<T>(this IUnitOfWork unitOfWork,
            int graphId,
            IMapper mapper,
            CancellationToken token = default)
            where T : IVertex
        {
            var graphEntity = await unitOfWork.GraphRepository.ReadAsync(graphId, token);
            var vertexEntities = (await unitOfWork.VerticesRepository
                .ReadVerticesByGraphIdAsync(graphId, token))
                .ToDictionary(x => x.Id);
            var ids = vertexEntities.Select(x => x.Key).ToReadOnly();
            var neighbors = (await unitOfWork.NeighborsRepository
                .ReadNeighboursForVerticesAsync(ids, token))
                .ToDictionary(x => x.Key, x => x.Value.Select(i => vertexEntities[i.NeighborId]).ToReadOnly());
            var informationDto = mapper.Map<GraphInformationModel>(graphEntity);
            var assembleDto = new GraphAssembleModel()
            {
                Dimensions = informationDto.Dimensions,
                Vertices = mapper.Map<VertexAssembleModel[]>(vertexEntities.Values),
                Neighborhood = neighbors
                    .ToDictionary(x => x.Key, x => mapper.Map<VertexAssembleModel[]>(x.Value)
                    .ToReadOnly())
            };
            var result = mapper.Map<IGraph<T>>(assembleDto);
            return new GraphModel<T>()
            {
                Graph = result,
                Id = graphEntity.Id,
                Name = graphEntity.Name
            };
        }

        public static async Task<IReadOnlyCollection<ICoordinate>> GetRangeAsync(this IUnitOfWork unitOfWork,
            int graphId,
            IMapper mapper,
            CancellationToken token = default)
        {
            var result = new List<ICoordinate>();
            var range = await unitOfWork.RangeRepository.ReadByGraphIdAsync(graphId, token);
            foreach (var x in range)
            {
                var vertex = await unitOfWork.VerticesRepository.ReadAsync(x.VertexId, token);
                var coordinates = mapper.Map<Coordinate>(vertex.Coordinates);
                result.Add(coordinates);
            }
            return result.AsReadOnly();
        }

        public static async Task<GraphModel<T>> AddGraphAsync<T>(this IUnitOfWork unitOfWork,
            IMapper mapper,
            CreateGraphRequest<T> graph,
            CancellationToken token = default)
            where T : IVertex, IEntity<int>
        {
            var graphEntity = mapper.Map<Graph>(graph);
            await unitOfWork.GraphRepository.CreateAsync(graphEntity, token);
            var vertices = mapper.Map<Vertex[]>(graph.Graph).ToReadOnly();
            vertices.ForEach((x, i) =>
            {
                x.Order = i;
                x.GraphId = graphEntity.Id;
            });
            await unitOfWork.VerticesRepository.CreateAsync(vertices, token);
            vertices.Zip(graph.Graph, (x, y) => (Entity: x, Vertex: y))
                .ForEach(x => x.Vertex.Id = x.Entity.Id);
            var neighbors = graph.Graph
                .SelectMany(x => x.Neighbours
                    .Select(n => new Neighbor() { VertexId = x.Id, NeighborId = x.Id }))
                    .ToReadOnly();
            await unitOfWork.NeighborsRepository.CreateAsync(neighbors, token);
            var result = mapper.Map<GraphModel<T>>(graph);
            result.Id = graphEntity.Id;
            return result;
        }
    }
}
