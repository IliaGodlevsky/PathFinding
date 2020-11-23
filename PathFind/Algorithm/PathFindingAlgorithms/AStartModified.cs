using Common.ValueRanges;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Algorithm.PathFindingAlgorithms
{
    /// <summary>
    /// A modified A* algorithm, that ignores vertices 
    /// that are far from optimal heuristic function result
    /// </summary>
    [Description("A* algorithm (modified)")]
    internal class AStarModified : AStarAlgorithm
    {
        public AStarModified(IGraph graph) : base(graph)
        {
            deletedVertices = new List<IVertex>();
            percentRange = new ValueRange(99, 0);
            if (!graph.IsDefault)
            {
                var partOfVertexToDelete = Math.Floor(Math.Log(graph.Size, 4));
                PersentOfFurthestVerticesToDelete = Convert.ToInt32(partOfVertexToDelete);
            }
            else
            {
                PersentOfFurthestVerticesToDelete = 100;
            }
        }

        public override void FindPath()
        {
            base.FindPath();
            deletedVertices.Clear();
        }

        protected override IVertex GetChippestUnvisitedVertex()
        {
            IVertex next = new DefaultVertex();

            verticesProcessQueue.Sort(CompareByHeuristic);

            var verticesToDelete = verticesProcessQueue.Take(VerticesCountToDelete);
            deletedVertices.AddRange(verticesToDelete);
            verticesProcessQueue.RemoveRange(0, VerticesCountToDelete);

            next = base.GetChippestUnvisitedVertex();

            if (next.IsDefault)
            {
                verticesProcessQueue = deletedVertices;
            }

            return base.GetChippestUnvisitedVertex();
        }

        protected virtual int CompareByHeuristic(IVertex v1, IVertex v2)
        {
            return HeuristicFunction(v2).CompareTo(HeuristicFunction(v1));
        }

        private int VerticesCountToDelete =>
            verticesProcessQueue.Count * PersentOfFurthestVerticesToDelete / 100;

        private int persentOfFurthestVerticesToDelete;

        /// <summary>
        /// Percent in percent points, f.e. 5, 10, 15
        /// </summary>
        private int PersentOfFurthestVerticesToDelete
        {
            get => persentOfFurthestVerticesToDelete;
            set
            {
                persentOfFurthestVerticesToDelete = value;
                if (!percentRange.IsInBounds(persentOfFurthestVerticesToDelete))
                {
                    persentOfFurthestVerticesToDelete =
                        percentRange.ReturnInBounds(persentOfFurthestVerticesToDelete);
                }
            }
        }

        private readonly ValueRange percentRange;
        private readonly List<IVertex> deletedVertices;
    }
}
