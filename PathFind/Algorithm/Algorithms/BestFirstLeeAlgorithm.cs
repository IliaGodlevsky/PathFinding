using Algorithm.Algorithms;
using GraphLib.Extensions;
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
            HeuristicFunction = vertex => vertex.CalculateChebyshevDistanceTo(Graph.End);
        }

        protected override IVertex NextVertex
        {
            get
            {
                var orderedVertices = verticesQueue.
                    OrderBy(vertex => vertex.AccumulatedCost);
                verticesQueue = new Queue<IVertex>(orderedVertices);

                return base.NextVertex;
            }
        }

        protected override double CreateWave()
        {
            return base.CreateWave() + HeuristicFunction(CurrentVertex);
        }
    }
}
