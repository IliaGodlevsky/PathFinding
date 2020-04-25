using GraphLibrary.Graph;
using GraphLibrary.Vertex;
using System;

namespace GraphLibrary.Algorithm
{
    /// <summary>
    /// An euristic dijkstra's algorithm. Uses distance of the current top
    /// to the destination top as a correction of top value
    /// </summary>
    public class AStarAlgorithm : DijkstraAlgorithm
    {
        public Func<IVertex, IVertex, double> HeuristicFunction;

        public AStarAlgorithm(AbstractGraph graph) : base(graph)
        {

        }

        protected override double GetPathValue(IVertex neighbour, IVertex vertex)
            => HeuristicFunction(neighbour, vertex);
    }
}
