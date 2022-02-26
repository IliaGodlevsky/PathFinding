using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using Algorithm.Сompanions.Interface;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Realizations.GraphPaths
{
    public sealed class GraphPath : IGraphPath
    {
        public IReadOnlyList<IVertex> Path => path.Value;

        public double Cost => pathCost.Value;

        public int Length => pathLength.Value;

        public GraphPath(IParentVertices parentVertices, IEndPoints endPoints)
            : this(parentVertices, endPoints, new DefaultStepRule())
        {

        }

        public GraphPath(IParentVertices parentVertices,
            IEndPoints endPoints, IStepRule stepRule)
        {
            path = new Lazy<IVertex[]>(GetPath);
            pathCost = new Lazy<double>(ExtractPathCost);
            pathLength = new Lazy<int>(GetPathLength);
            this.parentVertices = parentVertices;
            this.endPoints = endPoints;
            this.stepRule = stepRule;
        }

        private IVertex[] GetPath()
        {
            var path = ExtractPath().ToArray();
            return path.Contains(endPoints.Source) ? path : Array.Empty<IVertex>();
        }

        private IEnumerable<IVertex> ExtractPath()
        {
            var vertex = endPoints.Target;
            yield return vertex;
            var parent = parentVertices.GetParentOrNullVertex(vertex);
            while (parent.IsNeighbour(vertex))
            {
                yield return parent;
                vertex = parent;
                parent = parentVertices.GetParentOrNullVertex(vertex);
            }
        }

        private double ExtractPathCost()
        {
            if (Path.Count == 0)
            {
                return default;
            }

            double GetCost(int i)
            {
                return stepRule.CalculateStepCost(Path[i], Path[i + 1]);
            }

            return Enumerable.Range(0, Path.Count - 1).Sum(GetCost);
        }

        private int GetPathLength()
        {
            if (Path.Count == 0)
            {
                return default;
            }
            return Path.Count - 1;
        }

        private readonly IParentVertices parentVertices;
        private readonly IEndPoints endPoints;
        private readonly IStepRule stepRule;

        private readonly Lazy<int> pathLength;
        private readonly Lazy<double> pathCost;
        private readonly Lazy<IVertex[]> path;
    }
}