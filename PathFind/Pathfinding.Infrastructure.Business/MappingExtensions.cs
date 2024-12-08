﻿using Newtonsoft.Json;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business
{
    internal static class MappingExtensions
    {
        public static Statistics ToStatisitcs(this RunStatisticsSerializationModel model)
        {
            return new()
            {
                Algorithm = model.Algorithm,
                Cost = model.Cost,
                Visited = model.Visited,
                Heuristics = model.Heuristics,
                StepRule = model.StepRule,
                Steps = model.Steps,
                ResultStatus = model.ResultStatus,
                Elapsed = model.Elapsed.TotalMilliseconds,
                Weight = model.Weight
            };
        }

        public static IReadOnlyCollection<Statistics> ToStatistics(this IEnumerable<RunStatisticsSerializationModel> models)
        {
            return models.Select(x => x.ToStatisitcs()).ToList().AsReadOnly();
        }

        public static IReadOnlyCollection<Statistics> ToStatistics(this IEnumerable<RunStatisticsModel> models)
        {
            return models.Select(x => x.ToStatistics()).ToList().AsReadOnly();
        }

        public static RunStatisticsSerializationModel ToSerializionModel(this RunStatisticsModel model)
        {
            return new()
            {
                Algorithm = model.Algorithm,
                Cost = model.Cost,
                Elapsed = model.Elapsed,
                Heuristics = model.Heuristics,
                ResultStatus = model.ResultStatus,
                StepRule = model.StepRule,
                Steps = model.Steps,
                Visited = model.Visited,
                Weight = model.Weight
            };
        }

        public static IReadOnlyCollection<RunStatisticsSerializationModel> ToSerializationModels(this IEnumerable<RunStatisticsModel> models)
        {
            return models.Select(x => x.ToSerializionModel()).ToList().AsReadOnly();
        }

        public static Statistics ToStatistics(this RunStatisticsModel model)
        {
            return new()
            {
                GraphId = model.GraphId,
                Algorithm = model.Algorithm,
                Cost = model.Cost,
                Elapsed = model.Elapsed.TotalMilliseconds,
                Id = model.Id,
                Visited = model.Visited,
                Heuristics = model.Heuristics,
                StepRule = model.StepRule,
                Steps = model.Steps,
                ResultStatus = model.ResultStatus,
                Weight = model.Weight
            };
        }

        public static Statistics ToStatistics(this CreateStatisticsRequest request)
        {
            return new()
            {
                Algorithm = request.Algorithm,
                ResultStatus = request.ResultStatus,
                StepRule = request.StepRule,
                Steps = request.Steps,
                Heuristics = request.Heuristics,
                Cost = request.Cost,
                Elapsed = request.Elapsed.TotalMilliseconds,
                GraphId = request.GraphId,
                Visited = request.Visited,
                Weight = request.Weight
            };
        }

        public static T ToVertex<T>(this Vertex vertexEntity)
            where T : IVertex, IEntity<long>, new()
        {
            return new T()
            {
                Id = vertexEntity.Id,
                IsObstacle = vertexEntity.IsObstacle,
                Position = vertexEntity.Coordinates.ToCoordinates(),
                Cost = new VertexCost(vertexEntity.Cost, 
                    new(vertexEntity.UpperValueRange, vertexEntity.LowerValueRange)),
            };
        }

        public static IReadOnlyCollection<T> ToVertices<T>(this IEnumerable<Vertex> entities)
            where T : IVertex, IEntity<long>, new()
        {
            return entities.Select(x => x.ToVertex<T>()).ToList().AsReadOnly();
        }

        public static PathfindingRangeModel ToRangeModel(this PathfindingRange entity)
        {
            return new()
            {
                Id = entity.Id,
                IsSource = entity.IsSource,
                IsTarget = entity.IsTarget,
                Order = entity.Order,
                GraphId = entity.GraphId,
                VertexId = entity.VertexId
            };
        }

        public static RunStatisticsModel ToRunStatisticsModel(this Statistics entity)
        {
            return new()
            {
                Id = entity.Id,
                GraphId = entity.GraphId,
                Heuristics = entity.Heuristics,
                Weight = entity.Weight,
                StepRule = entity.StepRule,
                Steps = entity.Steps,
                Elapsed = TimeSpan.FromMilliseconds(entity.Elapsed),
                Visited = entity.Visited,
                Algorithm = entity.Algorithm,
                Cost = entity.Cost,
                ResultStatus = entity.ResultStatus
            };
        }

        public static IReadOnlyCollection<RunStatisticsModel> ToRunStatisticsModels(this IEnumerable<Statistics> entities)
        {
            return entities.Select(x => x.ToRunStatisticsModel()).ToList().AsReadOnly();
        }

        public static Graph ToGraphEntity<T>(this CreateGraphRequest<T> request)
            where T : IVertex
        {
            return new()
            {
                Name = request.Name,
                Neighborhood = request.Neighborhood,
                SmoothLevel = request.SmoothLevel,
                Dimensions = JsonConvert.SerializeObject(request.Graph.DimensionsSizes),
                Status = request.Status
            };
        }

        public static Vertex ToVertexEntity<T>(this T vertex)
            where T : IVertex, IEntity<long>
        {
            return new()
            {
                Id = vertex.Id,
                Coordinates = vertex.Position.ToStringCoordinates(),
                UpperValueRange = vertex.Cost.CostRange.UpperValueOfRange,
                LowerValueRange = vertex.Cost.CostRange.LowerValueOfRange,
                Cost = vertex.Cost.CurrentCost,
                IsObstacle = vertex.IsObstacle
            };
        }

        public static VertexSerializationModel ToSerializationModel<T>(this T vertex)
            where T : IVertex
        {
            var cost = vertex.Cost;
            return new()
            {
                IsObstacle = vertex.IsObstacle,
                Cost = new VertexCostModel()
                {
                    Cost = cost.CurrentCost,
                    UpperValueOfRange = cost.CostRange.UpperValueOfRange,
                    LowerValueOfRange = cost.CostRange.LowerValueOfRange
                },
                Position = new() { Coordinate = vertex.Position.CoordinatesValues}
            };
        }

        public static IReadOnlyCollection<VertexSerializationModel> ToSerializationModels<T>(this IEnumerable<T> vertices)
            where T : IVertex
        {
            return vertices.Select(x => x.ToSerializationModel<T>()).ToList().AsReadOnly();
        }

        public static IReadOnlyCollection<Vertex> ToVertexEntities<T>(this IEnumerable<T> vertices)
            where T : IVertex, IEntity<long>
        {
            return vertices.Select(x => x.ToVertexEntity()).ToList().AsReadOnly();
        }

        public static GraphInformationModel ToGraphInformationModel(this Graph graph)
        {
            return new()
            {
                Name = graph.Name,
                Neighborhood = graph.Neighborhood,
                Dimensions = JsonConvert.DeserializeObject<int[]>(graph.Dimensions),
                Id = graph.Id,
                SmoothLevel = graph.SmoothLevel,
                Status = graph.Status
            };
        }

        public static IReadOnlyCollection<GraphInformationModel> ToInformationModels(this IEnumerable<Graph> entities)
        {
            return entities.Select(x => x.ToGraphInformationModel()).ToList().AsReadOnly();
        }

        public static Graph ToGraphEntity(this GraphInformationModel model)
        {
            return new()
            {
                Name = model.Name,
                Neighborhood = model.Neighborhood,
                SmoothLevel = model.SmoothLevel,
                Status = model.Status,
                Dimensions = model.Dimensions.ToDimensionsString(),
                Id = model.Id,
            };
        }

        public static T ToVertex<T>(this VertexSerializationModel model)
            where T : IVertex, new()
        {
            var cost = model.Cost;
            return new T()
            {
                Cost = new VertexCost(cost.Cost, new(cost.UpperValueOfRange, cost.LowerValueOfRange)),
                IsObstacle = model.IsObstacle,
                Position = new Coordinate(model.Position.Coordinate)
            };
        }

        public static IReadOnlyCollection<T> ToVertices<T>(this IEnumerable<VertexSerializationModel> vertices)
            where T : IVertex, new()
        {
            return vertices.Select(x => x.ToVertex<T>()).ToList().AsReadOnly();
        }

        public static string ToStringCoordinates(this Coordinate coordinate)
        {
            var array = coordinate.CoordinatesValues.ToList();
            return JsonConvert.SerializeObject(array);
        }

        public static Coordinate ToCoordinates(this string coordinate)
        {
            var deserialized = JsonConvert.DeserializeObject<List<int>>(coordinate);
            return new Coordinate(deserialized);
        }

        public static int[] ToDimensionSizes(this string dimensions)
        {
            return JsonConvert.DeserializeObject<int[]>(dimensions);
        }

        public static string ToDimensionsString(this IReadOnlyList<int> dimensions)
        {
            return JsonConvert.SerializeObject(dimensions);
        }
    }
}
