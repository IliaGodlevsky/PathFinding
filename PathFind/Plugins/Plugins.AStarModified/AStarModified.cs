using Algorithm.Extensions;
using AssembleClassesLib.Attributes;
using Common;
using Common.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.StepRules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plugins.AStarModified
{
    [ClassName("A* algorithm (modified)")]
    public sealed class AStarModified : AStarAlgorithm.AStarAlgorithm
    {
        public AStarModified(IGraph graph, IEndPoints endPoints)
            : this(graph, endPoints, new DefaultStepRule())
        {

        }

        public AStarModified(IGraph graph, IEndPoints endPoints, IStepRule stepRule)
            : base(graph, endPoints, stepRule)
        {
            PercentOfFarthestVerticesToDelete
                = CalculatePercentOfFarthestVerticesToDelete;
            deletedVertices = new Queue<IVertex>();
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
                verticesQueue = verticesQueue.Except(verticesToDelete.AsEnumerable());

                var next = base.NextVertex;
                if (next.IsDefault())
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

        private int VerticesCountToDelete =>
            verticesQueue.Count * PercentOfFarthestVerticesToDelete / 100;

        private int percentOfFarthestVerticesToDelete;

        private int PercentOfFarthestVerticesToDelete
        {
            get => percentOfFarthestVerticesToDelete;
            set
            {
                percentOfFarthestVerticesToDelete = value;
                if (!percentValueRange.Contains(percentOfFarthestVerticesToDelete))
                {
                    percentOfFarthestVerticesToDelete =
                        percentValueRange.ReturnInRange(percentOfFarthestVerticesToDelete);
                }
            }
        }

        private int CalculatePercentOfFarthestVerticesToDelete
        {
            get
            {
                var partOfVertexToDelete = Math.Floor(Math.Log(graph.Size + 1, 4));
                return Convert.ToInt32(partOfVertexToDelete);
            }
        }

        private readonly ValueRange percentValueRange = new ValueRange(99, 0);
        private readonly Queue<IVertex> deletedVertices;
    }
}
