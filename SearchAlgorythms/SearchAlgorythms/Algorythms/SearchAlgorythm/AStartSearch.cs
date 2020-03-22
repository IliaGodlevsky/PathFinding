using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;
using System.Collections.Generic;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    
    public class AStarSearch : DijkstraAlgorythm
    {
        public delegate double HeuristicHandler(IGraphTop neighbour, IGraphTop top);

        public HeuristicHandler Heuristic;

        public AStarSearch(IGraphTop end, IGraph graph) : base(end, graph)
        {

        }

        public override double GetPathValue(IGraphTop neighbour, IGraphTop top)
        {
            return base.GetPathValue(neighbour, top) + Heuristic(neighbour, end);
        }

    }
}
