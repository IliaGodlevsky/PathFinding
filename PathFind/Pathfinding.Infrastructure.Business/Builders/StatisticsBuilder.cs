using Pathfinding.Domain.Core;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Requests.Create;
using System;

namespace Pathfinding.Infrastructure.Business.Builders
{
    public sealed class StatisticsBuilder
    {
        private readonly int graphId;
        private Domain.Core.Algorithms algorithm;
        private int pathLength;
        private StepRules? stepRules;
        private double? weight;
        private HeuristicFunctions? heuristic;
        private int visited;
        private RunStatuses status;
        private double cost;
        private TimeSpan elapsed;

        public static StatisticsBuilder TakeGraph(int graphId) => new(graphId);

        public StatisticsBuilder WithPath(IGraphPath path)
        {
            cost = path.Cost;
            pathLength = path.Count;
            return this;
        }

        public StatisticsBuilder WithAlgorithm(Domain.Core.Algorithms algorithm)
        {
            this.algorithm = algorithm;
            return this;
        }

        public StatisticsBuilder WithVisited(int visited)
        {
            this.visited = visited;
            return this;
        }

        public StatisticsBuilder WithStatus(RunStatuses status)
        {
            this.status = status;
            return this;
        }

        public StatisticsBuilder WithElapsed(TimeSpan elapsed)
        {
            this.elapsed = elapsed;
            return this;
        }

        public StatisticsBuilder WithHeuristics(HeuristicFunctions? heuristic)
        {
            this.heuristic = heuristic;
            return this;
        }

        public StatisticsBuilder WithWeight(double? weight)
        {
            this.weight = weight;
            return this;
        }

        public StatisticsBuilder WithStepRules(StepRules? stepRules)
        {
            this.stepRules = stepRules;
            return this;
        }

        public CreateStatisticsRequest Build()
        {
            return new()
            {
                GraphId = graphId,
                Algorithm = algorithm,
                StepRule = stepRules,
                Weight = weight,
                Heuristics = heuristic,
                Cost = cost,
                Steps = pathLength,
                Visited = visited,
                ResultStatus = status,
                Elapsed = elapsed,
            };
        }

        private StatisticsBuilder(int graphId) => this.graphId = graphId;
    }
}
