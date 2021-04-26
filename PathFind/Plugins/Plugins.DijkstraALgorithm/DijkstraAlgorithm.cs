using System.Collections.Generic;
using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations;
using Algorithm.Realizations.StepRules;
using Algorithm.Сompanions;
using AssembleClassesLib.Attributes;
using Common.Extensions;
using GraphLib.Interfaces;
using System.Linq;

namespace Plugins.DijkstraALgorithm
{
    [ClassName("Dijkstra's algorithm")]
    public class DijkstraAlgorithm : BaseWaveAlgorithm
    {
        public DijkstraAlgorithm(IGraph graph, IEndPoints endPoints)
            : this(graph, endPoints, new WalkStepRule(new LandscapeStepRule()))
        {

        }

        public DijkstraAlgorithm(IGraph graph, IEndPoints endPoints, IStepRule stepRule)
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
            return stepRule.CountStepCost(neighbour, CurrentVertex)
                   + accumulatedCosts.GetAccumulatedCost(CurrentVertex);
        }

        protected override void RelaxNeighbours(IEnumerable<IVertex> neighbours)
        {
            neighbours.ForEach(RelaxVertex);
        }
        #endregion

        protected readonly IStepRule stepRule;
    }
}
