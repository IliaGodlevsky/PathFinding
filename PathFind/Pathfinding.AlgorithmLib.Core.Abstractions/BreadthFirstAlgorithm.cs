﻿using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.AlgorithmLib.Core.Abstractions
{
    public abstract class BreadthFirstAlgorithm<TStorage> : WaveAlgorithm<TStorage>
        where TStorage : new()
    {
        public BreadthFirstAlgorithm(IPathfindingRange pathfindingRange)
            : base(pathfindingRange)
        {

        }

        protected override void RelaxVertex(IVertex vertex)
        {
            visited.Add(vertex);
            traces[vertex.Position] = CurrentVertex;
        }
    }
}
