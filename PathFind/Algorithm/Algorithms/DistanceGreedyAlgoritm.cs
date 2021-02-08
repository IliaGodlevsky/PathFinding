//using Algorithm.Extensions;
//using GraphLib.Interface;
//using GraphLib.NullObjects;
//using System.ComponentModel;

//namespace Algorithm.Algorithms
//{
//    [Description("Distance greedy algorithm")]
//    public class DistanceGreedyAlgoritm : DepthFirstAlgorithm
//    {
//        private IGraph graph;
//        public override IGraph Graph
//        {
//            get => graph;
//            set 
//            { 
//                graph = value; 
//                GreedyFunction = vertex => vertex.CalculateChebyshevDistanceTo(graph.End); 
//            }
//        }

//        public DistanceGreedyAlgoritm() : this(new NullGraph())
//        {

//        }

//        public DistanceGreedyAlgoritm(IGraph graph) : base(graph)
//        {

//        }
//    }
//}
