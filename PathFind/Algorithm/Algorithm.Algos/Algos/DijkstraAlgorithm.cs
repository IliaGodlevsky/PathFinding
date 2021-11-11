using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using Algorithm.Realizations.StepRules;
using Algorithm.Сompanions;
using Algorithm.Сompanions.Interface;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using Interruptable.Interface;
using Priority_Queue;
using System;

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

        protected override void Reset()
        {
            base.Reset();
            queue?.Clear();
            accumulatedCosts?.Clear();
        }

        protected override IVertex NextVertex => queue.DequeueOrNullVertex();

        protected override void PrepareForLocalPathfinding()
        {
            base.PrepareForLocalPathfinding();
            queue = new SimplePriorityQueue<IVertex, double>();
            accumulatedCosts = new Costs();
            accumulatedCosts.Reevaluate(CurrentEndPoints.Source, default);
        }

        protected virtual void RelaxVertex(IVertex vertex)
        {
            double relaxedCost = GetVertexRelaxedCost(vertex);
            double vertexCost = accumulatedCosts.GetCostOrDefault(vertex);
            if (vertexCost > relaxedCost)
            {
                accumulatedCosts.Reevaluate(vertex, relaxedCost);
                Enqueue(vertex, relaxedCost);
                parentVertices.Add(vertex, CurrentVertex);
            }
        }

        protected virtual void Enqueue(IVertex vertex, double value)
        {
            queue.EnqueueOrUpdatePriority(vertex, value);
        }

        protected virtual double GetVertexRelaxedCost(IVertex neighbour)
        {
            return stepRule.CalculateStepCost(neighbour, CurrentVertex)
                   + accumulatedCosts.GetCost(CurrentVertex);
        }

        protected override void RelaxNeighbours(IVertex[] neighbours)
        {
            neighbours.ForEach(RelaxVertex);
        }

        protected SimplePriorityQueue<IVertex, double> queue;
        protected ICosts accumulatedCosts;

        protected readonly IStepRule stepRule;
    }
}