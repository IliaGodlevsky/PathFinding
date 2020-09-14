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
            int startIndexOfRemoval = 0;
            int partOfRemoval = verticesProcessQueue.Count / 25;
            verticesProcessQueue.Sort((v1, v2) => HeuristicFunction(v2).CompareTo(HeuristicFunction(v1)));
            verticesProcessQueue.RemoveRange(startIndexOfRemoval, partOfRemoval);
            return base.GetChippestUnvisitedVertex();
        }
    }
}
