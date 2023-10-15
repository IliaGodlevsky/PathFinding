using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms.Localization;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.AlgorithmLib.Extensions;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Comparers;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Algorithms
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

        protected override IVertex GetNextVertex()
        {
            storage.OrderByDescending(v => heuristics[v.Position])
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
            return next;
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

        public override string ToString()
        {
            return Languages.IDAStarAlgorithm;
        }
    }
}