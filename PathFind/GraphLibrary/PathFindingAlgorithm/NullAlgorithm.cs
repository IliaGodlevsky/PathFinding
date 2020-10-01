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

        public IGraph Graph { get => NullGraph.Instance; set => _ = value; }

        public event AlgorithmEventHanlder OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnFinished;
        public event Action<IVertex> OnEnqueued;

        public void FindPath()
        {            
            OnStarted(this, new AlgorithmEventArgs());
            OnVertexVisited(NullVertex.Instance);
            OnFinished(this, new AlgorithmEventArgs());
            OnEnqueued(NullVertex.Instance);
            OnStarted = delegate { };
            OnVertexVisited = delegate { };
            OnFinished = delegate { };
            OnEnqueued = delegate { };
        }

        private static NullAlgorithm instance = null;
    }
}
