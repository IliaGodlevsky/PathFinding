using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using Algorithm.Сompanions;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Realizations.GraphPaths
{
    public sealed class GraphPath : IGraphPath
    {
        public IVertex[] Path => path.Value;

        public double PathCost => pathCost.Value;

        public int PathLength => pathLength.Value;

        public GraphPath(ParentVertices parentVertices, IEndPoints endPoints, IGraph graph)
            : this(parentVertices, endPoints, graph, new DefaultStepRule())
        {

        }

        public GraphPath(ParentVertices parentVertices,
            IEndPoints endPoints, IGraph graph, IStepRule stepRule)
        {
            path = new Lazy<IVertex[]>(GetPath);
            pathCost = new Lazy<double>(ExtractPathCost);
            pathLength = new Lazy<int>(() => Path.Count() - 1);
            this.parentVertices = parentVertices;
            this.graph = graph;
            this.endPoints = endPoints;
            this.stepRule = stepRule;
        }

        private IVertex[] GetPath()
        {
            var path = ExtractPath().ToArray();
            if (!path.ContainsElems(endPoints.Source, endPoints.Target))
            {
                path = new IVertex[] { };
            }
            return path;
        }

        private IEnumerable<IVertex> ExtractPath()
        {
            var vertex = endPoints.Target;
            yield return vertex;
            var parent = parentVertices.GetParent(vertex);
            while (graph.AreNeighbours(vertex, parent))
            {
                yield return parent;
                vertex = parent;
                parent = parentVertices.GetParent(vertex);
            }
        }

        private double ExtractPathCost()
        {
            double GetCost(int i)
            {
                return stepRule.CalculateStepCost(Path[i], Path[i + 1]);
            }

            return Enumerable.Range(0, Path.Length - 1).Sum(GetCost);
        }

        private readonly ParentVertices parentVertices;
        private readonly IGraph graph;
        private readonly IEndPoints endPoints;
        private readonly IStepRule stepRule;

        private readonly Lazy<int> pathLength;
        private readonly Lazy<double> pathCost;
        private readonly Lazy<IVertex[]> path;
    }
}
