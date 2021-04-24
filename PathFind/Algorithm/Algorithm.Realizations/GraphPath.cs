using Algorithm.Interfaces;
using Algorithm.Сompanions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.StepRules;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Realizations
{
    public sealed class GraphPath : IGraphPath
    {
        private IVertex[] Empty => new IVertex[] { };

        public IEnumerable<IVertex> Path
        {
            get
            {
                if (path == null)
                {
                    path = UnwindPath().ToArray();
                    if (!IsPathFormed())
                    {
                        path = Empty;
                    }
                    else
                    {
                        PathCost = Enumerable
                            .Range(0, path.Length - 1)
                            .ToArray()
                            .Sum(i => stepRule.StepCost(path[i], path[i + 1]));
                    }

                    path = path.Where(IsNotStart).ToArray();
                }

                return path;
            }
        }

        public int PathCost { get; private set; }

        public GraphPath(ParentVertices parentVertices, IEndPoints endPoints, IGraph graph)
            : this(parentVertices, endPoints, graph, new DefaultStepRule())
        {

        }

        public GraphPath(ParentVertices parentVertices, IEndPoints endPoints,
            IGraph graph, IStepRule stepRule)
        {
            this.parentVertices = parentVertices;
            this.graph = graph;
            this.endPoints = endPoints;
            this.stepRule = stepRule;
        }

        private IEnumerable<IVertex> UnwindPath()
        {
            var vertex = endPoints.End;
            yield return vertex;
            var parent = parentVertices.GetParent(vertex);
            while (graph.AreNeighbours(vertex, parent))
            {
                yield return parent;
                vertex = parent;
                parent = parentVertices.GetParent(vertex);
            }
        }

        private bool IsPathFormed()
        {
            return path.Length >= RequiredNumberOfVerticesForCompletePath;
        }

        private bool IsNotStart(IVertex vertex)
        {
            return !endPoints.Start.IsEqual(vertex);
        }

        private const int RequiredNumberOfVerticesForCompletePath = 2;

        private readonly ParentVertices parentVertices;
        private readonly IGraph graph;
        private readonly IEndPoints endPoints;
        private readonly IStepRule stepRule;

        private IVertex[] path;
    }
}
