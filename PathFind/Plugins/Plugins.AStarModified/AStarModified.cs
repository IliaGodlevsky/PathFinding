using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using Algorithm.Realizations.StepRules;
using AssembleClassesLib.Attributes;
using Common.ValueRanges;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plugins.AStarModified
{
    /// <summary>
    /// A modified version of A* algorithm
    /// </summary>
    /// <remarks>The modification is that the algorithm 
    /// removes the most distant vertices from its horisont 
    /// of search and searches only among vertices, that are 
    /// the closest to the target vertex</remarks>
    [ClassName("A* algorithm (modified)")]
    public sealed class AStarModified : AStarAlgorithm.AStarAlgorithm
    {
        public AStarModified(IGraph graph, IEndPoints endPoints)
            : this(graph, endPoints, new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public AStarModified(IGraph graph, IEndPoints endPoints, IStepRule stepRule, IHeuristic function)
            : base(graph, endPoints, stepRule, function)
        {
            percentValueRange = new InclusiveValueRange<int>(99, 0);
            percentOfFarthestVerticesToDelete = new Lazy<int>(CalculatePercentOfFarthestVerticesToDelete);
            deletedVertices = new Queue<IVertex>();
        }

        public AStarModified(IGraph graph, IEndPoints endPoints, IHeuristic function)
            : this(graph, endPoints, new DefaultStepRule(), function)
        {

        }

        public AStarModified(IGraph graph, IEndPoints endPoints, IStepRule stepRule)
            : this(graph, endPoints, stepRule, new ChebyshevDistance())
        {

        }

        protected override IVertex NextVertex
        {
            get
            {
                verticesQueue = verticesQueue
                    .OrderByDescending(CalculateHeuristic)
                    .ToQueue();

                var verticesToDelete = verticesQueue
                    .Take(VerticesCountToDelete)
                    .ToArray();

                deletedVertices.EnqueueRange(verticesToDelete);
                verticesQueue = verticesQueue.Except(verticesToDelete);

                var next = base.NextVertex;
                if (next.IsNullObject())
                {
                    verticesQueue = deletedVertices;
                    next = base.NextVertex;
                }
                return next;
            }
        }

        protected override void CompletePathfinding()
        {
            base.CompletePathfinding();
            deletedVertices.Clear();
        }

        private double CalculateHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, endPoints.Target);
        }

        private int VerticesCountToDelete => verticesQueue.Count * percentOfFarthestVerticesToDelete.Value / 100;

        private int CalculatePercentOfFarthestVerticesToDelete()
        {
            const int LogarithmBase = 4;
            int graphSize = graph.Size + 1;
            // this formula was found empirically (it means: in what power you need 
            // to raise the base of the logarithm to get the size of the graph)
            var percentOfVerticesToDelete = Math.Floor(Math.Log(graphSize, LogarithmBase));
            var partOfVerticesToDelete = Convert.ToInt32(percentOfVerticesToDelete);
            return percentValueRange.ReturnInRange(partOfVerticesToDelete);
        }

        private readonly InclusiveValueRange<int> percentValueRange;
        private readonly Lazy<int> percentOfFarthestVerticesToDelete;
        private readonly Queue<IVertex> deletedVertices;
    }
}
