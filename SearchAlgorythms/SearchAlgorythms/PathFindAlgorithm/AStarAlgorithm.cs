using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorithm
{
    /// <summary>
    /// An euristic dijkstra's algorithm. Uses distance of the current top
    /// to the destination top as a correction of top value
    /// </summary>
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
