using GraphLibrary.Vertex.Interface;
using System;

namespace GraphLibrary.PathFindingAlgorithm
{
    public class AStarAlgorithm : DijkstraAlgorithm
    {
        public AStarAlgorithm() : base()
        {

        }

        public Func<IVertex, double> HeuristicFunction { protected get; set; }

        protected override double RelaxFunction(IVertex neighbour, IVertex vertex)
        {
            return base.RelaxFunction(neighbour, vertex) + HeuristicFunction(vertex);
        }
    }
}
