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
    internal class BestFirstLeeAlgorithm : LeeAlgorithm
    {
        public Func<IVertex, double> HeuristicFunction { protected get; set; }

        public BestFirstLeeAlgorithm(IGraph graph) : base(graph)
        {
            HeuristicFunction = vertex => vertex.GetChebyshevDistanceTo(Graph.End);
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

        protected override double WaveFunction(IVertex vertex)
        {
            return base.WaveFunction(vertex) + HeuristicFunction(vertex);
        }
    }
}
