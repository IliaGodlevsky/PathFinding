using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using Algorithm.Realizations.StepRules;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Algos.Algos
{
    public class IDAStarAlgorithm : AStarAlgorithm
    {
        private const int PercentToDelete = 4;

        private readonly Dictionary<IVertex, double> deletedVertices;

        private int ToDeleteCount => queue.Count * PercentToDelete / 100;

        public IDAStarAlgorithm(IEndPoints endPoints)
            : this(endPoints, new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public IDAStarAlgorithm(IEndPoints endPoints, IStepRule stepRule, IHeuristic function)
            : base(endPoints, stepRule, function)
        {
            deletedVertices = new Dictionary<IVertex, double>();
        }

        protected override IVertex GetNextVertex()
        {
            var query = queue
                .OrderByDescending(heuristics.GetCost)
                .Take(ToDeleteCount)
                .Select(vertex => new { Vertex = vertex, Priority = queue.GetPriorityOrInfinity(vertex) })
                .ForEach(item =>
                {
                    queue.TryRemove(item.Vertex);
                    deletedVertices[item.Vertex] = item.Priority;
                });
            var next = base.GetNextVertex();
            if (next.IsNull())
            {
                deletedVertices.ForEach(node => queue.EnqueueOrUpdatePriority(node.Key, node.Value));
                deletedVertices.Clear();
                next = base.GetNextVertex();
            }
            return next;
        }

        protected override void Reset()
        {
            base.Reset();
            deletedVertices.Clear();
        }

        public override string ToString()
        {
            return "IDA* algorithm";
        }
    }
}