using GraphLibrary.Vertex.Interface;

namespace GraphLibrary.PathFindingAlgorithm
{
    /// <summary>
    /// A modified A* algorithm, that ignores vertices 
    /// that are far from optimal heuristic function result
    /// </summary>
    public class AStarModified : AStarAlgorithm
    {
        public AStarModified() : base()
        {

        }

        protected override IVertex GetChippestUnvisitedVertex()
        {
            verticesProcessQueue.Sort(CompareByHeuristic);
            verticesProcessQueue.RemoveRange(0, verticesProcessQueue.Count / 20);
            return base.GetChippestUnvisitedVertex();
        }

        private int CompareByHeuristic(IVertex vertex1, IVertex vertex2)
        {
            return HeuristicFunction(vertex2).CompareTo(HeuristicFunction(vertex1));
        }
    }
}
