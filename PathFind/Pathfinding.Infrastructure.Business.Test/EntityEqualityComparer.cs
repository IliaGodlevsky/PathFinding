using Pathfinding.Infrastructure.Business.Test.TestRealizations;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Extensions;

namespace Pathfinding.Infrastructure.Business.Test
{
    internal static class EntityEqualityComparer
    {
        public static bool AreEqual(CreateGraphRequest<TestVertex> request, GraphModel<TestVertex> model)
        {
            return request.Graph.SequenceEqual(model.Graph)
                && request.Name == model.Name
                && request.Neighborhood == model.Neighborhood
                && request.SmoothLevel == model.SmoothLevel
                && request.Graph.DimensionsSizes.SequenceEqual(model.Graph.DimensionsSizes);
        }

        public static bool AreEqual(CreateAlgorithmRunHistoryRequest request,
            AlgorithmRunHistoryModel model)
        {
            return AreEqual(request.GraphState, model.GraphState)
                && request.Run.AlgorithmId == model.Run.AlgorithmId
                && request.Run.GraphId == model.Run.GraphId
                && AreEqual(request.Statistics, model.Statistics);
        }

        public static bool AreEqual(RunStatisticsModel first, RunStatisticsModel second)
        {
            return first.ResultStatus == second.ResultStatus
                && first.Heuristics == second.Heuristics
                && first.Cost == second.Cost
                && first.Elapsed == second.Elapsed
                && first.Weight == second.Weight
                && first.StepRule == second.StepRule
                && first.Steps == second.Steps
                && first.Visited == second.Visited;
        }

        public static bool AreEqual(CreatePathfindingHistoryRequest<TestVertex> request,
            PathfindingHistoryModel<TestVertex> model)
        {
            return AreEqual(request.Graph, model.Graph)
                && request.Algorithms.Juxtapose(model.Algorithms, AreEqual)
                && request.Range.SequenceEqual(model.Range);
        }

        public static bool AreEqual(CreateGraphStateRequest request, GraphStateModel model)
        {
            return request.Obstacles.SequenceEqual(model.Obstacles)
                && request.Regulars.SequenceEqual(model.Regulars)
                && request.Range.SequenceEqual(model.Range)
                && request.Costs.SequenceEqual(model.Costs);
        }
    }
}
