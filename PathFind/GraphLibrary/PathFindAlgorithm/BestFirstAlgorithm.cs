using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorithm
{
    /// <summary>
    /// An euristic Li algorithm. Ignors tops that are far from destination top
    /// </summary>
    public class BestFirstAlgorithm : WidePathFindAlgorithm
    {
        public HeuristicHandler HeuristicFunction;

        public BestFirstAlgorithm(AbstractGraph graph) : base(graph)
        {
            
        }

        protected override bool IsSuitableForQueuing(IVertex vertex)
            => base.IsSuitableForQueuing(vertex)
            && HeuristicFunction(graph.Start, graph.End) >
            HeuristicFunction(vertex, graph.End);
    }
}
