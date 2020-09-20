using GraphLibrary.Graphs;
using GraphLibrary.EventArguments;
using GraphLibrary.Graphs.Interface;
using System;
using GraphLibrary.PathFindingAlgorithm.Interface;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.Vertex;

namespace GraphLibrary.PathFindingAlgorithm
{
    public sealed class NullAlgorithm : IPathFindingAlgorithm
    {
        private NullAlgorithm()
        {
            Graph = NullGraph.Instance;
            OnStarted = null;
            OnVertexVisited = null;
        }

        public static NullAlgorithm Instance
        {
            get
            {
                if (instance == null)
                    instance = new NullAlgorithm();
                return instance;
            }
        }

        public IGraph Graph { get; set; }

        public event AlgorithmEventHanlder OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnFinished;
        public event Action<IVertex> OnEnqueued;

        public void FindPath()
        {
            OnStarted?.Invoke(this, new AlgorithmEventArgs());
            OnVertexVisited?.Invoke(NullVertex.Instance);
            OnFinished?.Invoke(this, new AlgorithmEventArgs());
            OnEnqueued?.Invoke(NullVertex.Instance);
        }

        private static NullAlgorithm instance = null;
    }
}
