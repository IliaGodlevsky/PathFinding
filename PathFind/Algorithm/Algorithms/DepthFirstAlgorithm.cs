using Algorithm.Algorithms.Abstractions;
using Algorithm.EventArguments;
using Algorithm.Extensions;
using Algorithm.Handlers;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Algorithm.Algorithms
{
    [Description("Depth-first algorithm")]
    public class DepthFirstAlgorithm : BaseAlgorithm
    {
        public HeuristicHandler GreedyFunction { private get; set; }

        public DepthFirstAlgorithm(IGraph graph) : base(graph)
        {
            GreedyFunction = vertex => vertex.CalculateChebyshevDistanceTo(graph.Start);
            visitedVertices = new Stack<IVertex>();
        }

        public override void FindPath()
        {
            PrepareForPathfinding();
            while (!IsDestination)
            {
                PreviousVertex = CurrentVertex;
                CurrentVertex = NextVertex;
                ProcessCurrentVertex();
            }
            CompletePathfinding();
        }

        protected override IVertex NextVertex
        {
            get
            {
                var neighbours = CurrentVertex.GetUnvisitedNeighbours();
                bool IsLeastCostVertex(IVertex vertex) 
                    => GreedyFunction(vertex) == neighbours.Min(GreedyFunction);

                return neighbours
                    .ForEach(Enqueu)
                    .ToList()
                    .FindOrDefault(IsLeastCostVertex);
            }
        }

        protected override void CompletePathfinding()
        {
            visitedVertices.Clear();
            base.CompletePathfinding();
        }

        private void VisitCurrentVertex()
        {
            CurrentVertex.IsVisited = true;
            var args = new AlgorithmEventArgs(Graph, CurrentVertex);
            RaiseOnVertexVisitedEvent(args);
            visitedVertices.Push(CurrentVertex);
        }

        private void Enqueu(IVertex vertex)
        {
            var args = new AlgorithmEventArgs(Graph, vertex);
            RaiseOnVertexEnqueuedEvent(args);
        }

        private void ProcessCurrentVertex()
        {
            if (CurrentVertex.IsDefault)
            {
                CurrentVertex = visitedVertices.PopOrDefault();
            }
            else
            {
                VisitCurrentVertex();
                CurrentVertex.ParentVertex = PreviousVertex;
            }
        }

        private IVertex PreviousVertex { get; set; }

        private readonly Stack<IVertex> visitedVertices;
    }
}
