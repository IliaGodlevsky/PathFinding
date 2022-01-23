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
using System.Diagnostics;
using System.Linq;

namespace Algorithm.Algos.Algos
{
    /// <summary>
    /// A modified version of A* algorithm
    /// </summary>
    /// <remarks>The modification is that the algorithm 
    /// removes the most distant vertices from its horisont 
    /// of search and searches only among vertices, that are 
    /// the closest to the target vertex</remarks>
    [DebuggerDisplay("IDA* algorithm")]
    public class IDAStarAlgorithm : AStarAlgorithm,
        IAlgorithm, IInterruptableProcess, IInterruptable, IDisposable
    {
        private const int PercentToDelete = 4;

        public IDAStarAlgorithm(IEndPoints endPoints)
            : this(endPoints, new DefaultStepRule(), new ChebyshevDistance())
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
                var verticesToDelete = queue.TakeOrderedBy(ToDeleteCount, heuristics.GetCost).ToArray();
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

        private int ToDeleteCount => queue.Count * PercentToDelete / 100;

        protected override void Reset()
        {
            base.Reset();
            deletedVertices.Clear();
        }

        private readonly ICollection<Tuple<IVertex, double>> deletedVertices;
    }
}
