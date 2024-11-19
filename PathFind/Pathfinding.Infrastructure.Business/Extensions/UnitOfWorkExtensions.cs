using AutoMapper;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Business.Extensions
{
    public static class UnitOfWorkExtensions
    {
        public static async Task<GraphModel<T>> ReadGraphAsync<T>(this IUnitOfWork unitOfWork,
            int graphId,
            IMapper mapper,
            CancellationToken token = default)
            where T : IVertex
        {
            var graphEntity = await unitOfWork.GraphRepository.ReadAsync(graphId, token)
                .ConfigureAwait(false);
            var vertexEntities = await unitOfWork.VerticesRepository
                .ReadVerticesByGraphIdAsync(graphId, token).ConfigureAwait(false);
            var ids = vertexEntities.Select(x => x.Id).ToReadOnly();
            var informationDto = mapper.Map<GraphInformationModel>(graphEntity);
            var vertices = mapper.Map<VertexAssembleModel[]>(vertexEntities);
            var assembleDto = new GraphAssembleModel()
            {
                Dimensions = informationDto.Dimensions,
                Vertices = vertices
            };
            var runs = await unitOfWork.StatisticsRepository.ReadByGraphIdAsync(graphId, token)
                .ConfigureAwait(false);
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
            var range = await unitOfWork.RangeRepository.ReadByGraphIdAsync(graphId, token)
                .ConfigureAwait(false);
            foreach (var x in range)
            {
                var vertex = await unitOfWork.VerticesRepository.ReadAsync(x.VertexId, token)
                    .ConfigureAwait(false);
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
            where T : IVertex, IEntity<long>
        {
            var graphEntity = mapper.Map<Graph>(graph);
            await unitOfWork.GraphRepository.CreateAsync(graphEntity, token)
                .ConfigureAwait(false);
            var vertices = mapper.Map<Vertex[]>(graph.Graph).ToReadOnly();
            vertices.ForEach(x => x.GraphId = graphEntity.Id);
            await unitOfWork.VerticesRepository.CreateAsync(vertices, token)
                .ConfigureAwait(false);
            vertices.Zip(graph.Graph, (x, y) => (Entity: x, Vertex: y))
                .ForEach(x => x.Vertex.Id = x.Entity.Id);
            var result = mapper.Map<GraphModel<T>>(graph);
            result.Id = graphEntity.Id;
            return result;
        }
    }
}
