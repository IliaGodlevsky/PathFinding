using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using Algorithm.Realizations.StepRules;
using Algorithm.Сompanions;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using Interruptable.Interface;
using System;
using System.Linq;

namespace Algorithm.Algos.Algos
{
    /// <summary>
    /// Realization of Dijkstra's algorithm
    /// </summary>
    public class DijkstraAlgorithm : WaveAlgorithm,
        IAlgorithm, IInterruptableProcess, IInterruptable, IDisposable
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
                    .OrderBy(OrderFunction)
                    .Where(visitedVertices.IsNotVisited)
                    .ToQueue();

                return verticesQueue.DequeueOrNullVertex();
            }
        }

        protected override void PrepareForLocalPathfinding()
        {
            base.PrepareForLocalPathfinding();
            var vertices = graph.GetNotObstacles().Without(CurrentEndPoints.Source);
            accumulatedCosts = new AccumulatedCosts(vertices, double.PositiveInfinity);
            accumulatedCosts.Reevaluate(CurrentEndPoints.Source, default);
        }

        protected virtual void RelaxVertex(IVertex vertex)
        {
            double relaxedCost = GetVertexRelaxedCost(vertex);
            if (accumulatedCosts.Compare(vertex, relaxedCost) > 0)
            {
                Reevaluate(vertex, relaxedCost);
                parentVertices.Add(vertex, CurrentVertex);
            }
        }

        protected virtual void Reevaluate(IVertex vertex, double value)
        {
            accumulatedCosts.Reevaluate(vertex, value);
        }

        protected virtual double OrderFunction(IVertex vertex)
        {
            return accumulatedCosts.GetAccumulatedCost(vertex);
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
