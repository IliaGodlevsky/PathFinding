using Algorithm.Сalculations;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Algorithm.PathFindingAlgorithms
{
    [Description("Lee algorithm (heuristic)")]
    public class BestFirstLeeAlgorithm : LeeAlgorithm
    {
        public Func<IVertex, double> HeuristicFunction { protected get; set; }

        public BestFirstLeeAlgorithm(IGraph graph) : base(graph)
        {
            HeuristicFunction = vertex => DistanceCalculator.GetChebyshevDistance(vertex, Graph.End);
        }

        protected override IVertex GetNextVertex()
        {
            neighbourQueue = new Queue<IVertex>(neighbourQueue.OrderBy(vertex => vertex.AccumulatedCost));
            return base.GetNextVertex();
        }

        protected override double WaveFunction(IVertex vertex)
        {
            return base.WaveFunction(vertex) + HeuristicFunction(vertex);
        }
    }
}
