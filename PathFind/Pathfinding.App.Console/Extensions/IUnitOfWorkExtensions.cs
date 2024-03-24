using AutoMapper;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Create;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IUnitOfWorkExtensions
    {
        public static void AddHistory(this IUnitOfWork unitOfWork, IMapper mapper,
            IEnumerable<AlgorithmRunHistoryCreateDto> runHistories)
        {
            foreach (var runHistory in runHistories)
            {
                var runEntity = mapper.Map<AlgorithmRunEntity>(runHistory.Run);
                unitOfWork.RunRepository.Insert(runEntity);
                var graphState = mapper.Map<GraphStateEntity>(runHistory.GraphState);
                var subAlgorithms = mapper.Map<SubAlgorithmEntity[]>(runHistory.SubAlgorithms);
                var runStatistics = mapper.Map<StatisticsEntity>(runHistory.Statistics);
                subAlgorithms.ForEach(x => x.AlgorithmRunId = runEntity.Id);
                runStatistics.AlgorithmRunId = runEntity.Id;
                graphState.AlgorithmRunId = runEntity.Id;
                unitOfWork.GraphStateRepository.Insert(graphState);
                unitOfWork.SubAlgorithmRepository.Insert(subAlgorithms);
                unitOfWork.StatisticsRepository.Insert(runStatistics);
            }
        }

        public static IReadOnlyCollection<AlgorithmRunHistoryReadDto> GetAlgorithmRuns(this IUnitOfWork unitofWork,
            int graphId, IMapper mapper)
        {
            var algorithms = unitofWork.RunRepository.GetByGraphId(graphId);
            var dtos = mapper.Map<AlgorithmRunReadDto[]>(algorithms).ToReadOnly();
            var runHistories = new List<AlgorithmRunHistoryReadDto>();
            foreach (var dto in dtos)
            {
                var subAlgorithms = unitofWork.SubAlgorithmRepository.GetByAlgorithmRunId(dto.Id);
                var graphState = unitofWork.GraphStateRepository.GetByRunId(dto.Id);
                var runStatistics = unitofWork.StatisticsRepository.GetByAlgorithmRunId(dto.Id);
                runHistories.Add(new()
                {
                    Run = dto,
                    SubAlgorithms = mapper.Map<SubAlgorithmReadDto[]>(subAlgorithms),
                    GraphState = mapper.Map<GraphStateReadDto>(graphState),
                    Statistics = mapper.Map<RunStatisticsDto>(runStatistics)
                });
            }
            return runHistories.AsReadOnly();
        }

        public static IGraph<Vertex> CreateGraph(this IUnitOfWork unitOfWork,
            int graphId, IMapper mapper)
        {
            var graphEntity = unitOfWork.GraphRepository.Read(graphId);
            var vertexEntities = unitOfWork.VerticesRepository
                .GetVerticesByGraphId(graphId)
                .ToDictionary(x => x.Id);
            var ids = vertexEntities.Select(x => x.Key).ToReadOnly();
            var neighbors = unitOfWork.NeighborsRepository
                .GetNeighboursForVertices(ids)
                .ToDictionary(x => x.Key, x => x.Value.Select(i => vertexEntities[i.NeighborId]).ToReadOnly());
            var assembleDto = new GraphAssembleDto()
            {
                Width = graphEntity.Width,
                Length = graphEntity.Length,
                Vertices = mapper.Map<VertexAssembleDto[]>(vertexEntities.Values),
                Neighborhood = neighbors
                    .ToDictionary(x => x.Key, x => mapper.Map<VertexAssembleDto[]>(x.Value)
                    .ToReadOnly())
            };
            return mapper.Map<IGraph<Vertex>>(assembleDto);
        }

        public static IReadOnlyCollection<ICoordinate> GetRange(this IUnitOfWork unitOfWork, int graphId)
        {
            return unitOfWork.RangeRepository
                .GetByGraphId(graphId)
                .Select(x => unitOfWork.VerticesRepository.Read(x.VertexId))
                .Select(x => new Coordinate(x.X, x.Y))
                .ToReadOnly();
        }

        public static GraphReadDto AddGraph(this IUnitOfWork unitOfWork,
            IMapper mapper, IGraph<Vertex> graph)
        {
            var graphEntity = mapper.Map<GraphEntity>(graph);
            unitOfWork.GraphRepository.Insert(graphEntity);
            var vertices = mapper.Map<VertexEntity[]>(graph).ToReadOnly();
            vertices.ForEach((x, i) =>
            {
                x.Order = i;
                x.GraphId = graphEntity.Id;
            });
            unitOfWork.VerticesRepository.Insert(vertices);
            vertices.Zip(graph, (x, y) => (Entity: x, Vertex: y))
                .ForEach(x => x.Vertex.Id = x.Entity.Id);
            var neighbours = graph
                .SelectMany(x => x.Neighbours
                    .OfType<Vertex>()
                    .Select(n => new NeighborEntity() { VertexId = x.Id, NeighborId = n.Id }))
                    .ToReadOnly();
            unitOfWork.NeighborsRepository.Insert(neighbours);
            return new() { Id = graphEntity.Id, Graph = graph };
        }
    }
}
