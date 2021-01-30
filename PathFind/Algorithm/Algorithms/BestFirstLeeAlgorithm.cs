using Algorithm.Algorithms;
using Algorithm.Extensions;
using Algorithm.Handlers;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Algorithm.PathFindingAlgorithms
{
    [Description("Lee algorithm (heuristic)")]
    public class BestFirstLeeAlgorithm : LeeAlgorithm
    {
        public HeuristicHandler HeuristicFunction { get; set; }

        private IGraph graph;
        public override IGraph Graph
        {
            get => graph;
            set { graph = value; HeuristicFunction = vertex => vertex.CalculateChebyshevDistanceTo(graph.End); }
        }

        public BestFirstLeeAlgorithm() : this(new NullGraph())
        {

        }

        public BestFirstLeeAlgorithm(IGraph graph) : base(graph)
        {
            
        }

        public override void Reset()
        {
            base.Reset();
            HeuristicFunction = null;
        }

        protected override IVertex NextVertex
        {
            get
            {
                verticesQueue = verticesQueue
                    .OrderBy(vertex => vertex.AccumulatedCost)
                    .ToQueue();

                return base.NextVertex;
            }
        }

        protected override double CreateWave()
        {
            return base.CreateWave() + HeuristicFunction(CurrentVertex);
        }
    }
}
