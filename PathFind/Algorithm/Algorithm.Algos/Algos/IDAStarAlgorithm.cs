using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using Algorithm.Realizations.StepRules;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Algos.Algos
{
    public class IDAStarAlgorithm : AStarAlgorithm
    {
        private const int PercentToDelete = 4;

        private readonly Dictionary<IVertex, double> stashedVertices;

        private int ToStashCount => queue.Count * PercentToDelete / 100;

        public IDAStarAlgorithm(IEndPoints endPoints)
            : this(endPoints, new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public IDAStarAlgorithm(IEndPoints endPoints, IStepRule stepRule, IHeuristic function)
            : base(endPoints, stepRule, function)
        {
            stashedVertices = new Dictionary<IVertex, double>(new VertexEqualityComparer());
        }

        protected override IVertex GetNextVertex()
        {
            queue.OrderByDescending(v => heuristics[v.Position])
                .Take(ToStashCount)
                .Select(vertex => (Vertex: vertex, Priority: queue.GetPriorityOrInfinity(vertex)))
                .ForEach(item =>
                {
                    queue.TryRemove(item.Vertex);
                    stashedVertices[item.Vertex] = item.Priority;
                });
            var next = queue.TryFirstOrNullVertex();
            if (next.HasNoNeighbours())
            {
                stashedVertices.ForEach(item => queue.EnqueueOrUpdatePriority(item.Key, item.Value));
                stashedVertices.Clear();
                next = queue.TryFirstOrDeadEndVertex();
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
            return "IDA* algorithm";
        }
    }
}