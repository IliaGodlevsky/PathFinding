using AutoMapper;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.Entities;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
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
        public static IReadOnlyCollection<AlgorithmReadDto> GetAlgorithms(this IUnitOfWork unitofWork,
            int graphId, IMapper mapper)
        {
            var algorithms = unitofWork.AlgorithmsRepository.GetByGraphId(graphId);
            var dtos = mapper.Map<AlgorithmReadDto[]>(algorithms).ToReadOnly();
            foreach(var dto in dtos)
            {
                var subAlgorithms = unitofWork.SubAlgorithmRepository.GetByAlgorithmId(dto.Id);
                dto.SubAlgorithms = mapper.Map<SubAlgorithmReadDto[]>(subAlgorithms).ToReadOnly();
            }
            return dtos;
        }

        public static IGraph<Vertex> CreateGraph(this IUnitOfWork unitOfWork,
            int graphId, IMapper mapper)
        {
            var graphEntity = unitOfWork.GraphRepository.Read(graphId);
            var vertexEntities = unitOfWork.VerticesRepository
                .GetVerticesByGraphId(graphId)
                .OrderBy(x => x.Order)
                .ToDictionary(x => x.Id);
            var ids = vertexEntities.Select(x => x.Key).ToArray();
            var neighbors = unitOfWork.NeighborsRepository
                .GetNeighboursForVertices(ids)
                .ToDictionary(x => x.Key, x => x.Value.Select(i => vertexEntities[i.NeighborId]).ToReadOnly());
            var readDto = new GraphAssembleDto()
            {
                Width = graphEntity.Width,
                Length = graphEntity.Length,
                Vertices = mapper.Map<VertexAssembleDto[]>(vertexEntities.Values).ToReadOnly(),
                Neighborhood = neighbors.ToDictionary(x => x.Key, x => (IReadOnlyCollection<VertexAssembleDto>)mapper.Map<VertexAssembleDto[]>(x.Value).ToReadOnly())
            };
            return mapper.Map<IGraph<Vertex>>(readDto);
        }

        public static IReadOnlyCollection<ICoordinate> GetRange(this IUnitOfWork unitOfWork, int graphId)
        {
            return unitOfWork.RangeRepository
                .GetByGraphId(graphId)
                .OrderBy(x => x.Position)
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
            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i].Order = i;
                vertices[i].GraphId = graphEntity.Id;
            }
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
