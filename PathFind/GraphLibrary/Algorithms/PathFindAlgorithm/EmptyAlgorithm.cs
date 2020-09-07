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

        public event AlgorithmEventHanlder OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnFinished;

        public IEnumerable<IVertex> FindPath()
        {
            Graph = null;
            OnStarted = null;
            OnVertexVisited = null;
            OnStarted?.Invoke(this, 
                new AlgorithmEventArgs());
            OnVertexVisited?.Invoke(null);
            OnFinished?.Invoke(this, 
                new AlgorithmEventArgs());
            return this.GetFoundPath();
        }
    }
}
