﻿using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using Algorithm.Realizations.StepRules;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Algorithm.Algos.Algos
{
    [DebuggerDisplay("IDA* algorithm")]
    [Description("IDA* algorithm")]
    public class IDAStarAlgorithm : AStarAlgorithm
    {
        private const int PercentToDelete = 4;

        private readonly ICollection<Tuple<IVertex, double>> deletedVertices;

        private int ToDeleteCount => queue.Count * PercentToDelete / 100;

        public IDAStarAlgorithm(IEndPoints endPoints)
            : this(endPoints, new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public IDAStarAlgorithm(IEndPoints endPoints, IStepRule stepRule, IHeuristic function)
            : base(endPoints, stepRule, function)
        {
            deletedVertices = new HashSet<Tuple<IVertex, double>>();
        }

        protected override IVertex GetNextVertex()
        {
            var verticesToDelete = queue.TakeOrderedBy(ToDeleteCount, heuristics.GetCost);
            var tuples = queue.ToTuples(verticesToDelete, heuristics.GetCost).ToList();
            queue.RemoveRange(verticesToDelete);
            deletedVertices.AddRange(tuples);
            var next = base.GetNextVertex();
            if (next.IsNull())
            {
                queue.EnqueueRange(deletedVertices);
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
    }
}