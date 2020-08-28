using System;
using System.Linq;
using GraphLibrary.Graph;
using GraphLibrary.PathFindAlgorithm;
using GraphLibrary.Vertex;

namespace GraphLibrary.Algorithm
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
            var neighbours = !vertex.Neighbours.Any(vert => vert.IsVisited) 
                ? vertex.Neighbours : vertex.Neighbours.Where(vert => !vert.IsVisited).ToList();
            neighbours = neighbours.OrderBy(vert => Guid.NewGuid()).ToList();
            if (neighbours.Any())
                return neighbours.Find(vert => GreedyFunction(vert) == neighbours.Min(GreedyFunction));            
            return null;
        }
    }
}
