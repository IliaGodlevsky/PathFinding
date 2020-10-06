using GraphLibrary.ValueRanges;
using GraphLibrary.Vertex;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.PathFindingAlgorithm
{
    /// <summary>
    /// A modified A* algorithm, that ignores vertices 
    /// that are far from optimal heuristic function result
    /// </summary>
    public class AStarModified : AStarAlgorithm
    {
        private int persentOfFurthestVerticesToDelete;
        /// <summary>
        /// Percent in percent points, f.e. 5, 10, 15
        /// </summary>
        private int PersentOfFurthestToDelete
        {
            get => persentOfFurthestVerticesToDelete;
            set
            {
                persentOfFurthestVerticesToDelete = value;
                if (!percentRange.IsInBounds(persentOfFurthestVerticesToDelete))
                    persentOfFurthestVerticesToDelete = 
                        percentRange.ReturnInBounds(persentOfFurthestVerticesToDelete);
            }
        }

        public AStarModified() : base()
        {
            deletedVertices = new List<IVertex>();
            percentRange = new ValueRange(99, 0);
            PersentOfFurthestToDelete = 5;
        }

        protected override IVertex GetChippestUnvisitedVertex()
        {
            IVertex next = NullVertex.Instance;
            verticesProcessQueue.Sort((v1, v2) => HeuristicFunction(v2).CompareTo(HeuristicFunction(v1)));
            deletedVertices.AddRange(verticesProcessQueue.Take(VerticesCountToDelete));
            verticesProcessQueue.RemoveRange(0, VerticesCountToDelete);
            next = base.GetChippestUnvisitedVertex();
            if (ReferenceEquals(next, NullVertex.Instance))
                verticesProcessQueue = deletedVertices;
            return base.GetChippestUnvisitedVertex();
        }

        private int VerticesCountToDelete => 
            verticesProcessQueue.Count * PersentOfFurthestToDelete / 100;

        private readonly ValueRange percentRange;
        private readonly List<IVertex> deletedVertices;
    }
}
