using GraphLibrary.Vertex.Interface;

namespace GraphLibrary.PathFindingAlgorithm
{
    public class AStarModified : AStarAlgorithm
    {
        public AStarModified() : base()
        {

        }

        protected override IVertex GetChippestUnvisitedVertex()
        {
            verticesProcessQueue.Sort((v1, v2) => HeuristicFunction(v2).CompareTo(HeuristicFunction(v1)));
            verticesProcessQueue.RemoveRange(0, verticesProcessQueue.Count / 25);
            return base.GetChippestUnvisitedVertex();
        }
    }
}
