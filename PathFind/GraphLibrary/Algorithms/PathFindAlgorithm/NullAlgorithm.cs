using GraphLibrary.AlgorithmArgs;
using GraphLibrary.Collection;
using GraphLibrary.Common.Extensions;
using GraphLibrary.Model.Vertex;
using GraphLibrary.Vertex;
using System;
using System.Collections.Generic;

namespace GraphLibrary.Algorithm
{
    public class NullAlgorithm : IPathFindingAlgorithm
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
            OnVertexVisited?.Invoke(NullVertex.GetInstance());
            OnFinished?.Invoke(this, 
                new AlgorithmEventArgs());
            return this.GetFoundPath();
        }
    }
}
