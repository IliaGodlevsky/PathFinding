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
            var partOfVertexToDelete = Math.Floor(Math.Log(graph.Size + 1, 4));
            PersentOfFurthestVerticesToDelete = Convert.ToInt32(partOfVertexToDelete);
        }

        public override void FindPath()
        {
            base.FindPath();
            deletedVertices.Clear();
        }

        protected override IVertex ChippestUnvisitedVertex
        {
            get
            {
                IVertex next = new DefaultVertex();

                verticesQueue.Sort(CompareByHeuristic);

                var verticesToDelete = verticesQueue.Take(VerticesCountToDelete);
                deletedVertices.AddRange(verticesToDelete);
                verticesQueue.RemoveRange(0, VerticesCountToDelete);

                next = base.ChippestUnvisitedVertex;

                if (next.IsDefault)
                {
                    verticesQueue = deletedVertices;
                }

                return base.ChippestUnvisitedVertex;
            }
        }

        protected virtual int CompareByHeuristic(IVertex v1, IVertex v2)
        {
            return HeuristicFunction(v2).CompareTo(HeuristicFunction(v1));
        }

        private int VerticesCountToDelete =>
            verticesQueue.Count * PersentOfFurthestVerticesToDelete / 100;

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
