//using Algorithm.Base;
//using Algorithm.EventArguments;
//using Algorithm.Extensions;
//using Common.Extensions;
//using GraphLib.Extensions;
//using GraphLib.Interface;
//using GraphLib.NullObjects;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;

//namespace Algorithm.Algorithms
//{
//    [Description("Lee algorithm")]
//    public class LeeAlgorithm : BaseAlgorithm
//    {
//        public LeeAlgorithm() : this(new NullGraph())
//        {

//        }

//        public LeeAlgorithm(IGraph graph) : base(graph)
//        {
//            verticesQueue = new Queue<IVertex>();
//        }

//        public override void FindPath()
//        {
//            PrepareForPathfinding();
//            do
//            {
//                ExtractNeighbours();
//                SpreadWaves();
//                CurrentVertex = NextVertex;
//                CurrentVertex.IsVisited = true;
//                var args = new AlgorithmEventArgs(Graph, CurrentVertex);
//                RaiseOnVertexVisitedEvent(args);
//            } while (!IsDestination());
//            CompletePathfinding();
//        }

//        protected override void CompletePathfinding()
//        {
//            base.CompletePathfinding();
//            verticesQueue.Clear();
//        }

//        protected override IVertex NextVertex
//        {
//            get
//            {
//                verticesQueue = verticesQueue
//                    .Where(vertex => !visitedVertices.ContainsKey(vertex.Position))
//                    .ToQueue();

//                return verticesQueue.DequeueOrDefault();
//            }
//        }

//        protected virtual double CreateWave()
//        {
//            return CurrentVertex.AccumulatedCost + 1;
//        }

//        private void SpreadWaves()
//        {
//            CurrentVertex
//                .GetUnvisitedNeighbours()
//                .Where(neighbour => neighbour.AccumulatedCost == 0)
//                .ForEach(neighbour =>
//                {
//                    neighbour.AccumulatedCost = CreateWave();
//                    neighbour.ParentVertex = CurrentVertex;
//                });
//        }

//        private void ExtractNeighbours()
//        {
//            var neighbours = CurrentVertex.GetUnvisitedNeighbours();

//            foreach (var neighbour in neighbours)
//            {
//                var args = new AlgorithmEventArgs(Graph, neighbour);
//                RaiseOnVertexEnqueuedEvent(args);
//                verticesQueue.Enqueue(neighbour);
//            }

//            verticesQueue = verticesQueue
//                .DistinctBy(vert => vert.Position)
//                .ToQueue();
//        }

//        protected Queue<IVertex> verticesQueue;
//    }
//}
