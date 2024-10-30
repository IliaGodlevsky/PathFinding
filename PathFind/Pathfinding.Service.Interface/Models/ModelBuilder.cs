using Pathfinding.Service.Interface.Requests.Create;
using System;

namespace Pathfinding.Service.Interface.Models
{
    public static class ModelBuilder
    {
        public static CreateStatisticsRequest CreateStatisticsRequest() => new CreateStatisticsRequest();

        public static CreateStatisticsRequest WithStatistics(this CreateStatisticsRequest request,
            int graphId, string algorithmName, IGraphPath path,
            int visited, string resultStatus, TimeSpan elapsed)
        {
            request.AlgorithmName = algorithmName;
            request.Cost = path.Cost;
            request.Elapsed = elapsed;
            request.Steps = path.Count;
            request.ResultStatus = resultStatus;
            request.Visited = visited;
            request.GraphId = graphId;
            return request;
        }
    }
}
