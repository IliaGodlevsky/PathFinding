using GraphLibrary.Collection;
using GraphLibrary.Vertex;
using System;

namespace GraphLibrary.Algorithm
{
    public class EmptyAlgorithm : IPathFindAlgorithm
    {
        public Graph Graph { get; set; }

        public event AlgorithmEventHanlder OnAlgorithmStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnAlgorithmFinished;

        public void FindDestionation()
        {
            Graph = null;
            OnAlgorithmStarted = null;
            OnVertexVisited = null;
            OnAlgorithmStarted?.Invoke();
            OnVertexVisited?.Invoke(null);
            OnAlgorithmFinished?.Invoke();            
        }
    }
}
