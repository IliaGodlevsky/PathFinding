using Common;
using GraphLib.Interface;
using GraphLib.NullObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Algorithm.Algorithms
{
    [Description("A* algorithm (modified)")]
    public class AStarModified : AStarAlgorithm
    {
        public override IGraph Graph
        {
            get => base.Graph;
            set
            {
                base.Graph = value;
                PercentOfFarthestVerticesToDelete
                    = CalculatePercentOfFarthestVerticesToDelete();
            }
        }

        public AStarModified() : this(new NullGraph())
        {

        }

        public AStarModified(IGraph graph) : base(graph)
        {
            deletedVertices = new List<IVertex>();
        }

        protected override IVertex NextVertex
        {
            get
            {
                IVertex next = new DefaultVertex();
                verticesQueue.Sort(CompareByHeuristic);
                var verticesToDelete
                    = verticesQueue.Take(VerticesCountToDelete);
                deletedVertices.AddRange(verticesToDelete);
                verticesQueue.RemoveRange(0, VerticesCountToDelete);
                next = base.NextVertex;
                if (next.IsDefault)
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

        private int CompareByHeuristic(IVertex v1, IVertex v2)
        {
            return HeuristicFunction(v2).CompareTo(HeuristicFunction(v1));
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
                if (!percentValueRange.IsInRange(percentOfFarthestVerticesToDelete))
                {
                    percentOfFarthestVerticesToDelete =
                        percentValueRange.ReturnInRange(percentOfFarthestVerticesToDelete);
                }
            }
        }

        private int CalculatePercentOfFarthestVerticesToDelete()
        {
            var partOfVertexToDelete = Math.Floor(Math.Log(Graph.Size + 1, 4));
            return Convert.ToInt32(partOfVertexToDelete);
        }

        private readonly ValueRange percentValueRange = new ValueRange(99, 0);
        private readonly List<IVertex> deletedVertices;
    }
}
