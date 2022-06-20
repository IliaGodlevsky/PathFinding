using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using Algorithm.Realizations.StepRules;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using GraphLib.Utility;
using Priority_Queue;
using System.Collections.Generic;

namespace Algorithm.Algos.Algos
{
    public class DijkstraAlgorithm : WaveAlgorithm
    {
        protected readonly SimplePriorityQueue<IVertex, double> queue;
        protected readonly IStepRule stepRule;

        public DijkstraAlgorithm(IEndPoints endPoints)
            : this(endPoints, new DefaultStepRule())
        {

        }

        public DijkstraAlgorithm(IEndPoints endPoints, IStepRule stepRule)
            : base(endPoints)
        {
            this.stepRule = stepRule;
            queue = new SimplePriorityQueue<IVertex, double>(new VertexEqualityComparer());
        }

        protected override IGraphPath CreateGraphPath()
        {
            return new GraphPath(parentVertices, CurrentEndPoints, stepRule);
        }

        protected override void Reset()
        {
            base.Reset();
            queue.Clear();
        }

        protected override IVertex GetNextVertex()
        {
            return queue.TryFirst(out var vertex)
                ? vertex
                : NullVertex.Instance;
        }

        protected override void PrepareForLocalPathfinding()
        {
            base.PrepareForLocalPathfinding();
            queue.EnqueueOrUpdatePriority(CurrentEndPoints.Source, default);
        }

        protected virtual void RelaxVertex(IVertex vertex)
        {
            double relaxedCost = GetVertexRelaxedCost(vertex);
            double vertexCost = GetVertexCurrentCost(vertex);
            if (vertexCost > relaxedCost)
            {
                Enqueue(vertex, relaxedCost);
                parentVertices.Add(vertex, CurrentVertex);
            }
        }

        protected virtual void Enqueue(IVertex vertex, double value)
        {
            queue.EnqueueOrUpdatePriority(vertex, value);
        }

        protected virtual double GetVertexCurrentCost(IVertex vertex)
        {
            return queue.GetPriorityOrInfinity(vertex);
        }

        protected virtual double GetVertexRelaxedCost(IVertex neighbour)
        {
            return stepRule.CalculateStepCost(neighbour, CurrentVertex)
                   + GetVertexCurrentCost(CurrentVertex);
        }

        protected override void RelaxNeighbours(IReadOnlyCollection<IVertex> neighbours)
        {
            neighbours.ForEach(RelaxVertex);
            queue.TryRemove(CurrentVertex);
        }

        public override string ToString()
        {
            return "Dijkstra's algorithm";
        }
    }
}