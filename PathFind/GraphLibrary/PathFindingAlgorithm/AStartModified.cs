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
            verticesProcessQueue.Sort((v1, v2) => HeuristicFunction(v2).CompareTo(HeuristicFunction(v1)));
            verticesProcessQueue.RemoveRange(0, verticesProcessQueue.Count / 20);
            return base.GetChippestUnvisitedVertex();
        }
    }
}
