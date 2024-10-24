﻿using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Comparers;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public class IDAStarAlgorithm : AStarAlgorithm
    {
        private readonly int stashPercent;

        private readonly Dictionary<IVertex, double> stashedVertices;

        private int ToStashCount => storage.Count * stashPercent / 100;

        public IDAStarAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : this(pathfindingRange, new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public IDAStarAlgorithm(IEnumerable<IVertex> pathfindingRange,
            IStepRule stepRule, IHeuristic function, int stashPercent = 4)
            : base(pathfindingRange, stepRule, function)
        {
            this.stashPercent = stashPercent;
            stashedVertices = new(VertexEqualityComparer.Interface);
        }

        protected override void MoveNextVertex()
        {
            storage.OrderBy(v => accumulatedCosts[v.Position])
                .Take(ToStashCount)
                .Select(GetStashItem)
                .ForEach(AddToStash);
            var next = storage.TryFirstOrNullVertex();
            if (next.HasNoNeighbours())
            {
                stashedVertices.ForEach(storage.EnqueueOrUpdatePriority);
                stashedVertices.Clear();
                next = storage.TryFirstOrThrowDeadEndVertexException();
            }
            CurrentVertex = next;
        }

        private void AddToStash((IVertex Vertex, double Priority) stash)
        {
            storage.TryRemove(stash.Vertex);
            stashedVertices[stash.Vertex] = stash.Priority;
        }

        private (IVertex Vertex, double Priority) GetStashItem(IVertex vertex)
        {
            return (Vertex: vertex, Priority: storage.GetPriorityOrInfinity(vertex));
        }

        protected override void DropState()
        {
            base.DropState();
            stashedVertices.Clear();
        }
    }
}