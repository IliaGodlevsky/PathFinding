using Pathfinding.Domain.Core;
using Pathfinding.Service.Interface.Requests.Create;
using System;

namespace Pathfinding.Service.Interface.Models
{
    public static class ModelBuilder
    {
        public static CreateStatisticsRequest CreateStatisticsRequest() => new CreateStatisticsRequest();

        public static CreateStatisticsRequest WithStatistics(this CreateStatisticsRequest request,
            int graphId, Domain.Core.Algorithms algorithm, IGraphPath path,
            int visited, RunStatuses status, TimeSpan elapsed)
        {
            request.Algorithm = algorithm;
            request.Cost = path.Cost;
            request.Elapsed = elapsed;
            request.Steps = path.Count;
            request.ResultStatus = status;
            request.Visited = visited;
            request.GraphId = graphId;
            return request;
        }
    }
}
