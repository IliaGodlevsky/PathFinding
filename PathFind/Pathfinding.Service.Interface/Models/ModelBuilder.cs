using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Models.Read;
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

        public static CreateAlgorithmRunHistoryRequest WithGraph<T>(this CreateAlgorithmRunHistoryRequest request, GraphModel<T> graph, IEnumerable<Coordinate> range)
            where T : IVertex
        {
            request.GraphState = new CreateGraphStateRequest()
            {
                Costs = graph.Graph.Select(x => (x.Position, x.Cost.CurrentCost)).ToArray(),
                Obstacles = graph.Graph.Where(x => x.IsObstacle).Select(x => x.Position).ToArray(),
                Regulars = graph.Graph.Where(x => !x.IsObstacle).Select(x => x.Position).ToArray(),
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
    }
}
