using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using System;

namespace Pathfinding.Service.Interface.Models
{
    public static class ModelBuilder
    {
        public static CreateAlgorithmRunHistoryRequest CreateRunHistoryRequest() => new CreateAlgorithmRunHistoryRequest();

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
