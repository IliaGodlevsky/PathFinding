using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using Algorithm.Realizations.StepRules;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using Interruptable.Interface;
using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using ValueRange;
using ValueRange.Extensions;

namespace Algorithm.Algos.Algos
{
    /// <summary>
    /// A modified version of A* algorithm
    /// </summary>
    /// <remarks>The modification is that the algorithm 
    /// removes the most distant vertices from its horisont 
    /// of search and searches only among vertices, that are 
    /// the closest to the target vertex</remarks>
    public class IDAStarAlgorithm : AStarAlgorithm,
        IAlgorithm, IInterruptableProcess, IInterruptable, IDisposable
    {
        private const int PercentOfVerticesToDelete = 5;

        public IDAStarAlgorithm(IEndPoints endPoints)
            : this( endPoints, new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public IDAStarAlgorithm(IEndPoints endPoints, IStepRule stepRule, IHeuristic function)
            : base(endPoints, stepRule, function)
        {
            deletedVertices = new HashSet<Tuple<IVertex, double>>();
        }

        protected override IVertex NextVertex
        {
            get
            {
                var verticesToDelete = queue
                    .TakeOrderedBy(VerticesCountToDelete, heuristics.GetCost);
                var tuples = queue.ToTuples(verticesToDelete, heuristics.GetCost).ToList();
                queue.RemoveRange(verticesToDelete);
                deletedVertices.AddRange(tuples);
                var next = base.NextVertex;
                if (next.IsNull())
                {
                    queue.EnqueueRange(deletedVertices);
                    deletedVertices.Clear();
                    next = base.NextVertex;
                }
                return next;
            }
        }

        protected override void Reset()
        {
            base.Reset();
            deletedVertices.Clear();
        }

        private int VerticesCountToDelete => queue.Count * PercentOfVerticesToDelete / 100;

        private readonly ICollection<Tuple<IVertex, double>> deletedVertices;
    }
}
