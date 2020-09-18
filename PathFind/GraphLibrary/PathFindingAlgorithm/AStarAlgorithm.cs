using GraphLibrary.Vertex.Interface;
using System;

namespace GraphLibrary.PathFindingAlgorithm
{
    /// <summary>
    /// 
    /// </summary>
    public class AStarAlgorithm : DijkstraAlgorithm
    {
        public AStarAlgorithm() : base()
        {

        }
        /// <summary>
        /// A heuristic function is a creative function that consists in organizing a selective pathfinding
        /// </summary>
        public Func<IVertex, double> HeuristicFunction { protected get; set; }

        protected override double Relax(IVertex neighbour, IVertex vertex)
        {
            return base.Relax(neighbour, vertex) + HeuristicFunction(vertex);
        }
    }
}
