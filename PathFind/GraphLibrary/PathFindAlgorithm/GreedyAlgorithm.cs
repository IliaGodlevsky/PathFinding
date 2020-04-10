using System;
using System.Linq;
using GraphLibrary.PathFindAlgorithm;
using SearchAlgorythms.Extensions.ListExtensions;
using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorithm
{
    /// <summary>
    /// Greedy algorithm. Each step looks for the chippest top and visit it
    /// </summary>
    public class GreedyAlgorithm : DeepPathFindAlgorithm
    {
        public Func<IVertex, double> GreedyFunction { get; set; }

        public GreedyAlgorithm(AbstractGraph graph) : base(graph)
        {
            
        }

        protected override IVertex GoNextVertex(IVertex vertex)
        {
            var neighbours = vertex.Neighbours.Count(vert => vert.IsVisited) == 0 
                ? vertex.Neighbours : vertex.Neighbours.Where(vert => !vert.IsVisited).ToList();
            neighbours.Shuffle();
            if (neighbours.Any())
            {
                double min = neighbours.Min(GreedyFunction);
                return neighbours.Find(vert => GreedyFunction(vert) == min);
            }
            return null;
        }
    }
}
