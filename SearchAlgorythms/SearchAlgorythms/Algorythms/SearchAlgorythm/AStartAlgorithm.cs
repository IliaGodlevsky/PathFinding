using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
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
