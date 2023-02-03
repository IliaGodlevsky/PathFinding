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
    internal class IDAStarAlgorithm : AStarAlgorithm
    {
        private const int PercentToDelete = 4;

        private readonly Dictionary<IVertex, double> stashedVertices;

        private int ToStashCount => storage.Count * PercentToDelete / 100;

        public IDAStarAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : this(pathfindingRange, new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public IDAStarAlgorithm(IEnumerable<IVertex> pathfindingRange, IStepRule stepRule, IHeuristic function)
            : base(pathfindingRange, stepRule, function)
        {
            stashedVertices = new (new VertexEqualityComparer());
        }

        protected override IVertex GetNextVertex()
        {
            storage.OrderByDescending(v => heuristics[v.Position])
                .Take(ToStashCount)
                .Select(vertex => (Vertex: vertex, Priority: storage.GetPriorityOrInfinity(vertex)))
                .ForEach(item =>
                {
                    storage.TryRemove(item.Vertex);
                    stashedVertices[item.Vertex] = item.Priority;
                });
            var next = storage.TryFirstOrNullVertex();
            if (next.HasNoNeighbours())
            {
                stashedVertices.ForEach(item => storage.EnqueueOrUpdatePriority(item.Key, item.Value));
                stashedVertices.Clear();
                next = storage.TryFirstOrDeadEndVertex();
            }
            return next;
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