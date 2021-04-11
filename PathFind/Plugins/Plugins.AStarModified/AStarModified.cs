using Algorithm.Extensions;
using Common;
using Common.Extensions;
using GraphLib.Base;
using GraphLib.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Plugins.AStarModified
{
    [Description("A* algorithm (modified)")]
    public sealed class AStarModified : AStarAlgorithm.AStarAlgorithm
    {
        public override IGraph Graph
        {
            get => base.Graph;
            set
            {
                base.Graph = value;
                PercentOfFarthestVerticesToDelete
                    = CalculatePercentOfFarthestVerticesToDelete;
            }
        }

        public AStarModified() : this(BaseGraph.NullGraph)
        {

        }

        public AStarModified(IGraph graph) : base(graph)
        {
            deletedVertices = new Queue<IVertex>();
        }

        protected override IVertex NextVertex
        {
            get
            {
                verticesQueue = verticesQueue
                    .OrderByDescending(CalculateHeuristic)
                    .ToQueue();

                var verticesToDelete = verticesQueue.Take(VerticesCountToDelete);
                deletedVertices.EnqueueRange(verticesToDelete);
                verticesQueue = verticesQueue.Except(verticesToDelete);

                IVertex next = base.NextVertex;
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
                var partOfVertexToDelete = Math.Floor(Math.Log(Graph.Size + 1, 4));
                return Convert.ToInt32(partOfVertexToDelete);
            }
        }

        private readonly ValueRange percentValueRange = new ValueRange(99, 0);
        private readonly Queue<IVertex> deletedVertices;
    }
}
