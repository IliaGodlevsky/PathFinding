using AutoMapper;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Business.Extensions
{
    public static class UnitOfWorkExtensions
    {
        public static async Task<IReadOnlyCollection<AlgorithmRunHistoryModel>> AddHistoryAsync(this IUnitOfWork unitOfWork,
            IMapper mapper,
            IEnumerable<CreateAlgorithmRunHistoryRequest> runHistories,
            CancellationToken token = default)
        {
            return Array.AsReadOnly(await runHistories.ToAsyncEnumerable()
                .SelectAwait(async runHistory =>
            {
                var runEntity = mapper.Map<AlgorithmRun>(runHistory.Run);
                await unitOfWork.RunRepository.CreateAsync(runEntity, token);
                var runStatistics = mapper.Map<Statistics>(runHistory.Statistics);
                runStatistics.AlgorithmRunId = runEntity.Id;
                var stats = await unitOfWork.StatisticsRepository.CreateAsync(runStatistics, token);
                var info = await unitOfWork.GraphRepository.ReadAsync(runHistory.Run.GraphId, token);
                return new AlgorithmRunHistoryModel()
                {
                    Run = mapper.Map<AlgorithmRunModel>(runEntity),
                    GraphInfo = mapper.Map<GraphInformationModel>(info),
                    Statistics = mapper.Map<RunStatisticsModel>(stats) with { AlgorithmId = runHistory.Statistics.AlgorithmId }
                };
            }).ToArrayAsync(token));
        }

        public static async Task<IReadOnlyCollection<AlgorithmRunHistoryModel>> GetAlgorithmRuns(this IUnitOfWork unitofWork,
            int graphId,
            IMapper mapper,
            CancellationToken token = default)
        {
            var algorithms = await unitofWork.RunRepository.ReadByGraphIdAsync(graphId, token);
            var readModels = mapper.Map<AlgorithmRunModel[]>(algorithms).ToReadOnly();
            var runHistories = new List<AlgorithmRunHistoryModel>();
            var info = await unitofWork.GraphRepository.ReadAsync(graphId, token);
            var mappedInfo = mapper.Map<GraphInformationModel>(info);
            foreach (var model in readModels)
            {
                var runStatistics = await unitofWork.StatisticsRepository.ReadByAlgorithmRunIdAsync(model.Id, token);
                runHistories.Add(new AlgorithmRunHistoryModel()
                {
                    GraphInfo = mappedInfo,
                    Run = model,
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
            var informationDto = mapper.Map<GraphInformationModel>(graphEntity);
            var vertices = mapper.Map<VertexAssembleModel[]>(vertexEntities.Values);
            var assembleDto = new GraphAssembleModel()
            {
                Dimensions = informationDto.Dimensions,
                Vertices = vertices
            };
            var runs = await unitOfWork.RunRepository.ReadByGraphIdAsync(graphId, token);
            var result = mapper.Map<IGraph<T>>(assembleDto);
            return new GraphModel<T>()
            {
                Graph = result,
                Id = graphEntity.Id,
                Name = graphEntity.Name,
                Neighborhood = graphEntity.Neighborhood,
                SmoothLevel = graphEntity.SmoothLevel,
                IsReadOnly = runs.Any()
            };
        }

        public static async Task<IReadOnlyCollection<PathfindingRangeModel>> GetRangeAsync(this IUnitOfWork unitOfWork,
            int graphId,
            IMapper mapper,
            CancellationToken token = default)
        {
            var result = new List<PathfindingRangeModel>();
            var range = await unitOfWork.RangeRepository.ReadByGraphIdAsync(graphId, token);
            foreach (var x in range)
            {
                var vertex = await unitOfWork.VerticesRepository.ReadAsync(x.VertexId, token);
                var coordinates = mapper.Map<Coordinate>(vertex.Coordinates);
                var model = mapper.Map<PathfindingRangeModel>(x);
                model.Position = coordinates;
                result.Add(model);
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
                x.GraphId = graphEntity.Id;
            });
            await unitOfWork.VerticesRepository.CreateAsync(vertices, token);
            vertices.Zip(graph.Graph, (x, y) => (Entity: x, Vertex: y))
                .ForEach(x => x.Vertex.Id = x.Entity.Id);
            var result = mapper.Map<GraphModel<T>>(graph);
            result.Id = graphEntity.Id;
            return result;
        }
    }
}
