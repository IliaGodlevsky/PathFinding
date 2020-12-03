using System;
using System.Linq;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.ComponentModel;
using Algorithm.Extensions;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using Algorithm.Algorithms.Abstractions;

namespace Algorithm.Algorithms
{
    /// <summary>
    /// Greedy algorithm. Each step looks for the best vertex and visits it
    /// </summary>
    [Description("Depth-first algorithm")]
    internal class DepthFirstAlgorithm : BaseAlgorithm
    {
        /// <summary>
        /// A function that selects the best vertex on the step
        /// </summary>
        public Func<IVertex, double> GreedyFunction { private get; set; }

        public DepthFirstAlgorithm(IGraph graph) : base(graph)
        {
            GreedyFunction = vertex => vertex.GetChebyshevDistanceTo(graph.Start);
            visitedVertices = new Stack<IVertex>();
        }

        public override void FindPath()
        {
            BeginPathfinding();
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
