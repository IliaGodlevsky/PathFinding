using SearchAlgorithms.Graph;
using SearchAlgorithms.Top;

namespace SearchAlgorithms.Algorithms.SearchAlgorithm
{
    
    public class AStarAlgorithm : DijkstraAlgorithm
    {
        public delegate double HeuristicHandler(IGraphTop neighbour, IGraphTop top);

        public HeuristicHandler Heuristic;

        public AStarAlgorithm(IGraphTop end, IGraph graph) : base(end, graph)
        {

        }

        public override double GetPathValue(IGraphTop neighbour, IGraphTop top)
        {
            return base.GetPathValue(neighbour, top) + Heuristic(neighbour, end);
        }

    }
}
