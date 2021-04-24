using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations;
using Algorithm.Сompanions;
using AssembleClassesLib.Attributes;
using Common.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.StepRules;
using System.Linq;

namespace Plugins.DijkstraALgorithm
{
    [ClassName("Dijkstra's algorithm")]
    public class DijkstraAlgorithm : BaseWaveAlgorithm
    {

        public DijkstraAlgorithm(IGraph graph, IEndPoints endPoints)
            : this(graph, endPoints, new DefaultStepRule())
        {

        }

        public DijkstraAlgorithm(IGraph graph,
            IEndPoints endPoints, IStepRule stepRule)
            : base(graph, endPoints)
        {
            this.stepRule = stepRule;
        }

        protected override IGraphPath CreateGraphPath()
        {
            return new GraphPath(parentVertices, endPoints, graph, stepRule);
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

        protected override void CompletePathfinding()
        {
            base.CompletePathfinding();
            verticesQueue.Clear();
        }

        protected override void PrepareForPathfinding()
        {
            base.PrepareForPathfinding();
            accumulatedCosts =
                new AccumulatedCostsWithExcept(
                    new AccumulatedCosts(graph, double.PositiveInfinity), endPoints.Start);
        }

        #region Relaxing
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
            var cost = stepRule.StepCost(neighbour, CurrentVertex);
            return cost + accumulatedCosts.GetAccumulatedCost(CurrentVertex);
        }

        protected override void RelaxNeighbours()
        {
            visitedVertices
                .GetUnvisitedNeighbours(CurrentVertex)
                .ForEach(RelaxVertex);
        }
        #endregion

        protected readonly IStepRule stepRule;


    }
}
