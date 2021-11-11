using Algorithm.Base;
using Algorithm.Interfaces;
using Algorithm.Сompanions;
using Algorithm.Сompanions.Interface;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using Interruptable.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Algos.Algos
{
    public class LeeAlgorithm : WaveAlgorithm,
        IAlgorithm, IInterruptableProcess, IInterruptable, IDisposable
    {
        public LeeAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
            : base(graph, endPoints)
        {
            verticesQueue = new Queue<IVertex>();
        }

        protected override void PrepareForLocalPathfinding()
        {
            base.PrepareForLocalPathfinding();
            accumulatedCosts = new AccumulatedCosts(0);
        }

        protected override void CompletePathfinding()
        {
            base.CompletePathfinding();
            verticesQueue.Clear();
        }

        protected override IVertex NextVertex => verticesQueue.DequeueOrNullVertex();

        protected virtual double CreateWave()
        {
            return accumulatedCosts.GetAccumulatedCost(CurrentVertex) + 1;
        }

        protected virtual void RelaxNeighbour(IVertex vertex)
        {
            Reevaluate(vertex, CreateWave());
            verticesQueue.Enqueue(vertex);
            parentVertices.Add(vertex, CurrentVertex);
        }

        protected virtual void Reevaluate(IVertex vertex, double value)
        {
            accumulatedCosts.Reevaluate(vertex, value);
        }

        protected bool VertexIsUnwaved(IVertex vertex)
        {
            return accumulatedCosts.GetAccumulatedCost(vertex) == 0;
        }

        protected Queue<IVertex> verticesQueue;
        protected IAccumulatedCosts accumulatedCosts;

        protected override void RelaxNeighbours(IVertex[] neighbours)
        {
            neighbours
                .Where(VertexIsUnwaved)
                .ForEach(RelaxNeighbour);
        }
    }
}
