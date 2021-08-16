using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using Algorithm.Realizations.StepRules;
using Algorithm.Сompanions;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Linq;

namespace Algorithm.Algos.Algos
{
    /// <summary>
    /// Realization of Dijkstra's algorithm
    /// </summary>
    public class DijkstraAlgorithm : WaveAlgorithm
    {
        public DijkstraAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
            : this(graph, endPoints, new DefaultStepRule())
        {

        }

        public DijkstraAlgorithm(IGraph graph, IIntermediateEndPoints endPoints, IStepRule stepRule)
            : base(graph, endPoints)
        {
            this.stepRule = stepRule;
        }

        protected override IGraphPath CreateGraphPath()
        {
            return new GraphPath(parentVertices, CurrentEndPoints, stepRule);
        }

        protected override IVertex NextVertex
        {
            get
            {
                verticesQueue = verticesQueue
                    .OrderBy(accumulatedCosts.GetAccumulatedCost)
                    .Where(visitedVertices.IsNotVisited)
                    .ToQueue();

                return verticesQueue.DequeueOrDefault();
            }
        }

        protected override void PrepareForLocalPathfinding(IEndPoints endPoints)
        {
            base.PrepareForLocalPathfinding(endPoints);
            var vertices = graph.GetNotObstacles().Except(endPoints.Source);
            accumulatedCosts = new AccumulatedCosts(vertices, double.PositiveInfinity);
            accumulatedCosts.Reevaluate(endPoints.Source, default);
        }

        protected virtual void RelaxVertex(IVertex vertex)
        {
            var relaxedCost = GetVertexRelaxedCost(vertex);
            if (accumulatedCosts.Compare(vertex, relaxedCost) > 0)
            {
                accumulatedCosts.Reevaluate(vertex, relaxedCost);
                parentVertices.Add(vertex, CurrentVertex);
            }
        }

        protected virtual double GetVertexRelaxedCost(IVertex neighbour)
        {
            return stepRule.CalculateStepCost(neighbour, CurrentVertex)
                   + accumulatedCosts.GetAccumulatedCost(CurrentVertex);
        }

        protected override void RelaxNeighbours(IVertex[] neighbours)
        {
            neighbours.ForEach(RelaxVertex);
        }

        protected readonly IStepRule stepRule;
    }
}
