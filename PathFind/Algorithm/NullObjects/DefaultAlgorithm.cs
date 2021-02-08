//using Algorithm.Attributes;
//using Algorithm.EventArguments;
//using Algorithm.Handlers;
//using Algorithm.Interface;
//using GraphLib.Interface;
//using GraphLib.NullObjects;

//namespace Algorithm.NullObjects
//{
//    [Filterable]
//    public sealed class DefaultAlgorithm : IAlgorithm
//    {
//        public event AlgorithmEventHandler OnStarted;
//        public event AlgorithmEventHandler OnVertexVisited;
//        public event AlgorithmEventHandler OnFinished;
//        public event AlgorithmEventHandler OnVertexEnqueued;

//        public IGraph Graph
//        {
//            get => new NullGraph();
//            set => _ = value;
//        }

//        public bool IsDefault => true;

//        public DefaultAlgorithm()
//        {

//        }

//        public DefaultAlgorithm(IGraph graph)
//        {
//            Graph = graph;
//        }

//        public void FindPath()
//        {
//            OnStarted?.Invoke(this, new AlgorithmEventArgs());
//            OnVertexVisited?.Invoke(this, new AlgorithmEventArgs());
//            OnVertexEnqueued?.Invoke(this, new AlgorithmEventArgs());
//            OnFinished?.Invoke(this, new AlgorithmEventArgs());
//        }

//        public void Reset()
//        {

//        }
//    }
//}
