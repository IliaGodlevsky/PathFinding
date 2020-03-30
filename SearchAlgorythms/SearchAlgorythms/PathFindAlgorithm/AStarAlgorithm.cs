using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorithm
{
    
    public class AStarAlgorithm : DijkstraAlgorithm
    {
        public delegate double HeuristicHandler(IGraphTop neighbour, IGraphTop top);

        public HeuristicHandler HeuristicFunction;

        public AStarAlgorithm(IGraphTop end, IGraph graph) : base(end, graph)
        {

        }

        public override double GetPathValue(IGraphTop neighbour, IGraphTop top)
        {
            return base.GetPathValue(neighbour, top) + HeuristicFunction(neighbour, end);
        }

    }
}
