using Algorithm.Extensions;
using GraphLib.Interface;
using GraphLib.NullObjects;
using System.ComponentModel;

namespace Algorithm.Algorithms
{
    [Description("A* algorithm")]
    public class AStarAlgorithm : DijkstraAlgorithm
    {
        public AStarAlgorithm() : this(new NullGraph())
        {

        }

        public AStarAlgorithm(IGraph graph) : base(graph)
        {

        }

        protected virtual double CalculateHeuristic(IVertex vertex)
        {
            return vertex.CalculateChebyshevDistanceTo(endPoints.End);
        }

        protected override double GetVertexRelaxedCost(IVertex neighbour)
        {
            return base.GetVertexRelaxedCost(neighbour) + CalculateHeuristic(CurrentVertex);
        }
    }
}
