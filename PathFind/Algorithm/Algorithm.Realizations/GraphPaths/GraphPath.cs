using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Realizations.GraphPaths
{
    using Traces = System.Collections.Generic.IReadOnlyDictionary<GraphLib.Interfaces.ICoordinate, GraphLib.Interfaces.IVertex>;

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
            var vertices = new List<IVertex>();
            var vertex = target;
            vertices.Add(vertex);
            var parent = traces.GetOrNullVertex(vertex.Position);
            while (AreNeighbours(parent, vertex))
            {
                vertices.Add(parent);
                vertex = parent;
                parent = traces.GetOrNullVertex(vertex.Position);
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

        public bool AreNeighbours(IVertex self, IVertex candidate)
        {
            return self.Neighbours.Any(vertex => ReferenceEquals(vertex, candidate));
        }

        public IEnumerator<ICoordinate> GetEnumerator()
        {
            return Path.Select(vertex => vertex.Position).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}