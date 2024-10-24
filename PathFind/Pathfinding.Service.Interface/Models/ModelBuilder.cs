using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Service.Interface.Models
{
    public static class ModelBuilder
    {
        public static CreateAlgorithmRunHistoryRequest CreateRunHistoryRequest() => new CreateAlgorithmRunHistoryRequest();

        public static CreateAlgorithmRunHistoryRequest WithSubAlgorithms(this CreateAlgorithmRunHistoryRequest request, IReadOnlyList<CreateSubAlgorithmRequest> subAlgorithms)
        {
            request.SubAlgorithms = subAlgorithms;
            return request;
        }

        public static CreateAlgorithmRunHistoryRequest WithGraph<T>(this CreateAlgorithmRunHistoryRequest request, IGraph<T> graph, IEnumerable<Coordinate> range)
            where T : IVertex
        {
            request.GraphState = new CreateGraphStateRequest()
            {
                Costs = graph.Select(x => (x.Position, x.Cost.CurrentCost)).ToArray(),
                Obstacles = graph.Where(x => x.IsObstacle).Select(x => x.Position).ToArray(),
                Regulars = graph.Where(x => !x.IsObstacle).Select(x => x.Position).ToArray(),
                Range = range.ToArray()
            };
            return request;
        }

        public static CreateAlgorithmRunHistoryRequest WithRun(this CreateAlgorithmRunHistoryRequest request, int graphId, string algorithmId)
        {
            request.Run = new CreateAlgorithmRunRequest()
            {
                GraphId = graphId,
                AlgorithmId = algorithmId
            };
            return request;
        }

        public static CreateAlgorithmRunHistoryRequest WithStatistics(this CreateAlgorithmRunHistoryRequest request,
            string algorithmId, IGraphPath path, int visited, string resultStatus, TimeSpan elapsed)
        {
            request.Statistics = new RunStatisticsModel()
            {
                AlgorithmId = algorithmId,
                Cost = path.Cost,
                Elapsed = elapsed,
                Steps = path.Count,
                ResultStatus = resultStatus,
                Visited = visited
            };
            return request;
        }

        public static CreateSubAlgorithmRequest CreateSubAlgorithmRequest() => new();

        public static CreateSubAlgorithmRequest WithPath(this CreateSubAlgorithmRequest request, IReadOnlyCollection<Coordinate> subPath)
        {
            request.Path = subPath;
            return request;
        }

        public static CreateSubAlgorithmRequest WithVisitedVertices(this CreateSubAlgorithmRequest request,
            IReadOnlyCollection<(Coordinate, IReadOnlyList<Coordinate>)> visited)
        {
            request.Visited = visited;
            return request;
        }

        public static CreateSubAlgorithmRequest WithOrder(this CreateSubAlgorithmRequest request,
            int order)
        {
            request.Order = order;
            return request;
        }

        public static CreateSubAlgorithmRequest AddTo(this CreateSubAlgorithmRequest request,
            ICollection<CreateSubAlgorithmRequest> collection)
        {
            collection.Add(request);
            return request;
        }
    }
}
