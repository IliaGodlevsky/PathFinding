using System;
using System.Linq;
using GraphLibrary.Extensions;
using GraphLibrary.PathFindAlgorithm;
using GraphLibrary.Vertex;

namespace GraphLibrary.Algorithm
{
    /// <summary>
    /// Greedy algorithm. Each step looks for the chippest vertex and visits it
    /// </summary>
    public class GreedyAlgorithm : DeepPathFindAlgorithm
    {
        public Func<IVertex, double> GreedyFunction { private get; set; }
        public GreedyAlgorithm() : base()
        {
            
        }

        protected override IVertex GoNextVertex(IVertex vertex)
        {
            var neighbours = vertex.GetUnvisitedNeighbours().ToList();
            return neighbours?.Find(vert 
                => GreedyFunction(vert) == neighbours.Min(GreedyFunction));
        }
    }
}
