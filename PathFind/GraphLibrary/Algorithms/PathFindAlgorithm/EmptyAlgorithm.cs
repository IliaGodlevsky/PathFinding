using GraphLibrary.AlgorithmArgs;
using GraphLibrary.Collection;
using GraphLibrary.Common.Extensions;
using GraphLibrary.Vertex;
using System;
using System.Collections.Generic;

namespace GraphLibrary.Algorithm
{
    public class EmptyAlgorithm : IPathFindingAlgorithm
    {
        public Graph Graph { get; set; }

        public event AlgorithmEventHanlder OnAlgorithmStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnAlgorithmFinished;

        public IEnumerable<IVertex> FindPath()
        {
            Graph = null;
            OnAlgorithmStarted = null;
            OnVertexVisited = null;
            OnAlgorithmStarted?.Invoke(this, new AlgorithmEventArgs());
            OnVertexVisited?.Invoke(null);
            OnAlgorithmFinished?.Invoke(this, new AlgorithmEventArgs());
            return this.GetFoundPath();
        }
    }
}
