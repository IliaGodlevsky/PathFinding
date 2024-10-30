using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using Priority_Queue;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public class BidirectDijkstraAlgorithm : BidirectWaveAlgorithm<SimplePriorityQueue<IVertex, double>>
    {
        protected readonly IStepRule stepRule;

        public BidirectDijkstraAlgorithm(IEnumerable<IVertex> pathfindingRange, IStepRule stepRule)
            : base(pathfindingRange)
        {
            this.stepRule = stepRule;
        }

        public BidirectDijkstraAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : this(pathfindingRange, new DefaultStepRule())
        {

        }

        protected override void PrepareForSubPathfinding((IVertex Source, IVertex Target) range)
        {
            base.PrepareForSubPathfinding(range);
            forwardStorage.EnqueueOrUpdatePriority(Range.Source, default);
            backwardStorage.EnqueueOrUpdatePriority(Range.Target, default);
        }

        protected override IGraphPath GetSubPath()
        {
            return new BidirectGraphPath(forwardTraces.ToDictionary(),
                backwardTraces.ToDictionary(), Intersection, stepRule);
        }

        protected override void DropState()
        {
            base.DropState();
            forwardStorage.Clear();
            backwardStorage.Clear();
        }

        protected override void MoveNextVertex()
        {
            var forward = forwardStorage.TryFirstOrThrowDeadEndVertexException();
            var backward = backwardStorage.TryFirstOrThrowDeadEndVertexException();
            Current = (forward, backward);
        }

        protected override void RelaxForwardVertex(IVertex vertex)
        {
            double relaxedCost = GetForwardVertexRelaxedCost(vertex);
            double vertexCost = GetForwardVertexCurrentCost(vertex);
            if (vertexCost > relaxedCost)
            {
                EnqueueForward(vertex, relaxedCost);
                if (Intersection == NullVertex.Instance
                    && backwardVisited.Contains(vertex))
                {
                    Intersection = vertex;
                }
                forwardTraces[vertex.Position] = Current.Forward;
            }
        }

        protected override void RelaxBackwardVertex(IVertex vertex)
        {
            double relaxedCost = GetBackwardVertexRelaxedCost(vertex);
            double vertexCost = GetBackwardVertexCurrentCost(vertex);
            if (vertexCost > relaxedCost)
            {
                EnqueueBackward(vertex, relaxedCost);
                if (Intersection == NullVertex.Instance
                    && forwardVisited.Contains(vertex))
                {
                    Intersection = vertex;
                }
                backwardTraces[vertex.Position] = Current.Backward;
            }
        }

        protected virtual void EnqueueForward(IVertex vertex, double value)
        {
            forwardStorage.EnqueueOrUpdatePriority(vertex, value);
        }

        protected virtual void EnqueueBackward(IVertex vertex, double value)
        {
            backwardStorage.EnqueueOrUpdatePriority(vertex, value);
        }

        protected virtual double GetForwardVertexCurrentCost(IVertex vertex)
        {
            return forwardStorage.GetPriorityOrInfinity(vertex);
        }

        protected virtual double GetBackwardVertexCurrentCost(IVertex vertex)
        {
            return backwardStorage.GetPriorityOrInfinity(vertex);
        }

        protected virtual double GetForwardVertexRelaxedCost(IVertex neighbour)
        {
            return stepRule.CalculateStepCost(neighbour, Current.Forward)
                   + GetForwardVertexCurrentCost(Current.Forward);
        }

        protected virtual double GetBackwardVertexRelaxedCost(IVertex neighbour)
        {
            return stepRule.CalculateStepCost(neighbour, Current.Backward)
                   + GetBackwardVertexCurrentCost(Current.Backward);
        }

        protected override void RelaxForwardNeighbours(IReadOnlyCollection<IVertex> neighbours)
        {
            base.RelaxForwardNeighbours(neighbours);
            forwardStorage.TryRemove(Current.Forward);
        }

        protected override void RelaxBackwardNeighbours(IReadOnlyCollection<IVertex> vertices)
        {
            base.RelaxBackwardNeighbours(vertices);
            backwardStorage.TryRemove(Current.Backward);
        }
    }
}
