using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.NullObjects;
using Shared.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.AlgorithmLib.Core.Realizations.GraphPaths
{
    using Traces = IReadOnlyDictionary<ICoordinate, IVertex>;

    public sealed class GraphPath : IGraphPath
    {
        private readonly Traces traces;
        private readonly IVertex target;
        private readonly IStepRule stepRule;

        private IReadOnlyList<IVertex> Path { get; }

        public double Cost { get; }

        public int Count => Path.Count == 0 ? 0 : Path.Count - 1;

        public GraphPath(Traces traces, IVertex target)
            : this(traces, target, new DefaultStepRule())
        {

        }

        public GraphPath(Traces traces, IVertex target, IStepRule stepRule)
        {
            this.traces = traces;
            this.target = target;
            this.stepRule = stepRule;
            Path = GetPath();
            Cost = GetPathCost();
        }

        private IReadOnlyList<IVertex> GetPath()
        {
            var vertices = new HashSet<IVertex>();
            var vertex = target;
            vertices.Add(vertex);
            var parent = GetParentOrNullVertex(vertex);
            while (AreNeighbours(parent, vertex))
            {
                vertices.Add(parent);
                vertex = parent;
                parent = GetParentOrNullVertex(vertex);
            }
            return vertices.ToReadOnly();
        }

        private double GetPathCost()
        {
            double totalCost = 0;
            for (int i = 0; i < Path.Count - 1; i++)
            {
                totalCost += stepRule.CalculateStepCost(Path[i], Path[i + 1]);
            }
            return totalCost;
        }

        private IVertex GetParentOrNullVertex(IVertex vertex)
        {
            return traces.GetOrDefault(vertex.Position, () => NullVertex.Instance);
        }

        private static bool AreNeighbours(IVertex self, IVertex candidate)
        {
            return self.Neighbours.Any(candidate.Equals);
        }

        public IEnumerator<ICoordinate> GetEnumerator()
        {
            for (int i = 0; i < Path.Count; i++)
            {
                yield return Path[i].Position;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}