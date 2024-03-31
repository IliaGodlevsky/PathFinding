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
using System.Runtime.Remoting.Messaging;

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
            var read = mapper.Map<AlgorithmRunReadDto[]>(algorithms).ToReadOnly();
            var runHistories = new List<AlgorithmRunHistoryReadDto>();
            foreach (var dto in read)
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

        public static GraphReadDto<T> CreateGraph<T>(this IUnitOfWork unitOfWork,
            int graphId, IMapper mapper)
            where T : IVertex
        {
            var graphEntity = unitOfWork.GraphRepository.Read(graphId);
            var vertexEntities = unitOfWork.VerticesRepository
                .GetVerticesByGraphId(graphId)
                .ToDictionary(x => x.Id);
            var ids = vertexEntities.Select(x => x.Key).ToReadOnly();
            var neighbors = unitOfWork.NeighborsRepository
                .GetNeighboursForVertices(ids)
                .ToDictionary(x => x.Key, x => x.Value.Select(i => vertexEntities[i.NeighborId]).ToReadOnly());
            var informationDto = mapper.Map<GraphInformationReadDto>(graphEntity);
            var assembleDto = new GraphAssembleDto()
            {
                Dimensions = informationDto.Dimensions,
                Vertices = mapper.Map<VertexAssembleDto[]>(vertexEntities.Values),
                Neighborhood = neighbors
                    .ToDictionary(x => x.Key, x => mapper.Map<VertexAssembleDto[]>(x.Value)
                    .ToReadOnly())
            };
            var result = mapper.Map<IGraph<T>>(assembleDto);
            return new() { Graph = result, Id = graphEntity.Id, Name = graphEntity.Name };
        }

        public static IReadOnlyCollection<ICoordinate> GetRange(this IUnitOfWork unitOfWork, int graphId)
        {
            return unitOfWork.RangeRepository
                .GetByGraphId(graphId)
                .Select(x => unitOfWork.VerticesRepository.Read(x.VertexId))
                .Select(x => new Coordinate(x.X, x.Y))
                .ToReadOnly();
        }

        public static GraphReadDto<T> AddGraph<T>(this IUnitOfWork unitOfWork,
            IMapper mapper, GraphCreateDto<T> graph)
            where T : IVertex
        {
            var graphEntity = mapper.Map<GraphEntity>(graph);
            unitOfWork.GraphRepository.Insert(graphEntity);
            var vertices = mapper.Map<VertexEntity[]>(graph.Graph).ToReadOnly();
            vertices.ForEach((x, i) =>
            {
                x.Order = i;
                x.GraphId = graphEntity.Id;
            });
            unitOfWork.VerticesRepository.Insert(vertices);
            vertices.Zip(graph.Graph, (x, y) => (Entity: x, Vertex: y))
                .ForEach(x => ((dynamic)x.Vertex).Id = x.Entity.Id);
            var neighbors = graph.Graph
                .SelectMany(x => x.Neighbours
                    .OfType<T>()
                    .Select(n => new NeighborEntity() { VertexId = ((dynamic)x).Id, NeighborId = ((dynamic)n).Id }))
                    .ToReadOnly();
            unitOfWork.NeighborsRepository.Insert(neighbors);
            return mapper.Map<GraphReadDto<T>>(graph) with { Id =  graphEntity.Id };
        }
    }
}
