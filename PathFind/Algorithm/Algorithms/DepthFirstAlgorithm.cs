using Algorithm.Algorithms.Abstractions;
using Algorithm.Extensions;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Algorithm.Algorithms
{

    [Description("Depth-first algorithm")]
    public class DepthFirstAlgorithm : BaseAlgorithm
    {
        public Func<IVertex, double> GreedyFunction { private get; set; }

        public DepthFirstAlgorithm(IGraph graph) : base(graph)
        {
            GreedyFunction = vertex => vertex.GetChebyshevDistanceTo(graph.Start);
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
                var neighbours = CurrentVertex.GetUnvisitedNeighbours().ToList();

                foreach (var neighbour in neighbours)
                {
                    RaiseOnVertexEnqueuedEvent(neighbour);
                }

                return neighbours.FindOrDefault(vert =>
                {
                    return GreedyFunction(vert) == neighbours.Min(GreedyFunction);
                });
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
            RaiseOnVertexVisitedEvent();
            visitedVertices.Push(CurrentVertex);
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
