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
        public HeuristicHandler HeuristicFunction;

        public AStarAlgorithm(AbstractGraph graph) : base(graph)
        {

        }

        protected override double GetPathValue(IVertex neighbour, IVertex vertex) 
            => base.GetPathValue(neighbour, vertex) + HeuristicFunction(neighbour, graph.End);
    }
}
