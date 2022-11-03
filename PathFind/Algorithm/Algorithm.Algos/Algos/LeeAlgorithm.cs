using Algorithm.Base;
using Algorithm.NullRealizations;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Algos.Algos
{
    public class LeeAlgorithm : WaveAlgorithm
    {
        private const int WaveCost = 1;

        protected Queue<IVertex> verticesQueue;
        protected readonly Dictionary<ICoordinate, double> accumulatedCosts;

        public LeeAlgorithm(IEndPoints endPoints)
            : base(endPoints)
        {
            verticesQueue = new Queue<IVertex>();
            accumulatedCosts = new Dictionary<ICoordinate, double>(new CoordinateEqualityComparer());
        }

        protected override void Reset()
        {
            base.Reset();
            accumulatedCosts.Clear();
            verticesQueue.Clear();
        }

        protected override IVertex GetNextVertex()
        {
            return verticesQueue.Count == 0
                ? DeadEndVertex.Interface
                : verticesQueue.Dequeue();
        }

        protected virtual double CreateWave()
        {
            if (accumulatedCosts.TryGetValue(CurrentVertex.Position, out var cost))
            {
                return cost + WaveCost;
            }
            return WaveCost;
        }

        protected virtual void RelaxNeighbour(IVertex vertex)
        {
            Reevaluate(vertex, CreateWave());
            verticesQueue.Enqueue(vertex);
            traces[vertex.Position] = CurrentVertex;
        }

        protected virtual void Reevaluate(IVertex vertex, double value)
        {
            accumulatedCosts[vertex.Position] = value;
        }

        protected bool IsVertexUnwaved(IVertex vertex)
        {
            if (accumulatedCosts.TryGetValue(vertex.Position, out double cost))
            {
                return cost == 0;
            }
            return true;
        }

        protected override void RelaxNeighbours(IReadOnlyCollection<IVertex> neighbours)
        {
            neighbours.Where(IsVertexUnwaved).ForEach(RelaxNeighbour);
        }

        public override string ToString()
        {
            return "Lee algorithm";
        }
    }
}
