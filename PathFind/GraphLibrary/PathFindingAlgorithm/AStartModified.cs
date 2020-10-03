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
        private int persentOfFurthestToDelete;
        /// <summary>
        /// Percent in percent points, f.e. 5, 10, 15
        /// </summary>
        public int PersentOfFurthestToDelete
        {
            get => persentOfFurthestToDelete;
            set
            {
                persentOfFurthestToDelete = value;
                if (!percentRange.IsInBounds(persentOfFurthestToDelete))
                    persentOfFurthestToDelete = 
                        percentRange.ReturnInBounds(persentOfFurthestToDelete);
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
            verticesProcessQueue.Sort(CompareByHeuristic);
            deletedVertices.AddRange(verticesProcessQueue.Take(UpperRange));
            verticesProcessQueue.RemoveRange(0, UpperRange);
            next = base.GetChippestUnvisitedVertex();
            if (ReferenceEquals(next, NullVertex.Instance))
                verticesProcessQueue = deletedVertices;
            return base.GetChippestUnvisitedVertex();
        }

        private int CompareByHeuristic(IVertex vertex1, IVertex vertex2)
        {
            return HeuristicFunction(vertex2).CompareTo(HeuristicFunction(vertex1));
        }

        private int UpperRange => verticesProcessQueue.Count * PersentOfFurthestToDelete / 100;

        private ValueRange percentRange;
        private List<IVertex> deletedVertices;
    }
}
