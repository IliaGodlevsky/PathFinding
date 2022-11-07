using Algorithm.Realizations.GraphPaths;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.GraphLib.Core.Interface;
using Priority_Queue;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Algorithms
{
    public class DijkstraAlgorithm : WaveAlgorithm<SimplePriorityQueue<IVertex, double>>
    {
        protected readonly IStepRule stepRule;

        public DijkstraAlgorithm(IPathfindingRange pathfindingRange)
            : this(pathfindingRange, new DefaultStepRule())
        {

        }

        public DijkstraAlgorithm(IPathfindingRange pathfindingRange, IStepRule stepRule)
            : base(pathfindingRange)
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

        protected override void RelaxVertex(IVertex vertex)
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
            base.RelaxNeighbours(neighbours);
            storage.TryRemove(CurrentVertex);
        }

        public override string ToString()
        {
            return "Dijkstra's algorithm";
        }
    }
}