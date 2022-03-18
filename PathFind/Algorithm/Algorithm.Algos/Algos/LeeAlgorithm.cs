using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Сompanions;
using Algorithm.Сompanions.Interface;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Algorithm.Algos.Algos
{
    [DebuggerDisplay("Lee algorithm")]
    [Description("Lee algorithm")]
    public class LeeAlgorithm : WaveAlgorithm
    {
        protected Queue<IVertex> verticesQueue;
        protected readonly ICosts accumulatedCosts;

        public LeeAlgorithm(IEndPoints endPoints)
            : base(endPoints)
        {
            verticesQueue = new Queue<IVertex>();
            accumulatedCosts = new Costs();
        }

        protected override void Reset()
        {
            base.Reset();
            accumulatedCosts.Clear();
            verticesQueue.Clear();
        }

        protected override IVertex GetNextVertex()
        {
            return verticesQueue.DequeueOrNullVertex();
        }

        protected virtual double CreateWave()
        {
            return accumulatedCosts.GetCostOrDefault(CurrentVertex, default) + 1;
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

        protected bool IsVertexUnwaved(IVertex vertex)
        {
            return accumulatedCosts.GetCostOrDefault(vertex, default) == 0;
        }

        protected override void RelaxNeighbours(IReadOnlyCollection<IVertex> neighbours)
        {
            neighbours.Where(IsVertexUnwaved).ForEach(RelaxNeighbour);
        }
    }
}
