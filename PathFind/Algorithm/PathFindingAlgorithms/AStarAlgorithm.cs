using Algorithm.Сalculations;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;
using System.ComponentModel;

namespace Algorithm.PathFindingAlgorithms
{
    /// <summary>
    /// An informed pathfinding algorithm, or a 
    /// best-first pathfinding algorithm, searching 
    /// the path using heuristic function
    /// </summary>
    [Description("A* algorithm")]
    public class AStarAlgorithm : DijkstraAlgorithm
    {
        public AStarAlgorithm(IGraph graph) : base(graph)
        {
            HeuristicFunction = vertex => DistanceCalculator.CalculateChebyshevDistance(vertex, graph.End);
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
