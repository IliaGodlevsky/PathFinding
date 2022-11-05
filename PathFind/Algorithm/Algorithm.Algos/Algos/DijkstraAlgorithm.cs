using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.GraphPaths;
using Algorithm.Realizations.StepRules;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using Priority_Queue;
using System.Collections.Generic;

namespace Algorithm.Algos.Algos
{
    public class DijkstraAlgorithm : WaveAlgorithm<SimplePriorityQueue<IVertex, double>>
    {
        protected readonly IStepRule stepRule;

        public DijkstraAlgorithm(IEndPoints endPoints)
            : this(endPoints, new DefaultStepRule())
        {

        }

        public DijkstraAlgorithm(IEndPoints endPoints, IStepRule stepRule)
            : base(endPoints)
        {
            this.stepRule = stepRule;
        }

        protected override IGraphPath CreateGraphPath()
        {
            return new GraphPath(traces.ToReadOnly(), CurrentRange.Target, stepRule);
        }

        protected override void DropState()
        {
            base.DropState();
            storage.Clear();
        }

        protected override IVertex GetNextVertex()
        {
            return storage.TryFirstOrDeadEndVertex();
        }

        protected override void PrepareForSubPathfinding(Range range)
        {
            base.PrepareForSubPathfinding(range);
            storage.EnqueueOrUpdatePriority(CurrentRange.Source, default);
        }

        protected virtual void RelaxVertex(IVertex vertex)
        {
            double relaxedCost = GetVertexRelaxedCost(vertex);
            double vertexCost = GetVertexCurrentCost(vertex);
            if (vertexCost > relaxedCost)
            {
                Enqueue(vertex, relaxedCost);
                traces[vertex.Position] = CurrentVertex;
            }
        }

        protected virtual void Enqueue(IVertex vertex, double value)
        {
            storage.EnqueueOrUpdatePriority(vertex, value);
        }

        protected virtual double GetVertexCurrentCost(IVertex vertex)
        {
            return storage.GetPriorityOrInfinity(vertex);
        }

        protected virtual double GetVertexRelaxedCost(IVertex neighbour)
        {
            return stepRule.CalculateStepCost(neighbour, CurrentVertex)
                   + GetVertexCurrentCost(CurrentVertex);
        }

        protected override void RelaxNeighbours(IReadOnlyCollection<IVertex> neighbours)
        {
            neighbours.ForEach(RelaxVertex);
            storage.TryRemove(CurrentVertex);
        }

        public override string ToString()
        {
            return "Dijkstra's algorithm";
        }
    }
}