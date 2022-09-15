using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using Algorithm.Сompanions.Interface;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm.Realizations.GraphPaths
{
    public sealed class GraphPath : IGraphPath
    {
        private readonly IParentVertices parentVertices;
        private readonly IEndPoints endPoints;
        private readonly IStepRule stepRule;

        private readonly Lazy<double> pathCost;
        private readonly Lazy<IReadOnlyList<IVertex>> path;

        private IReadOnlyList<IVertex> Path => path.Value;

        public double Cost => pathCost.Value;

        public int Count => Path.Count == 0 ? 0 : Path.Count - 1;

        public GraphPath(IParentVertices parentVertices, IEndPoints endPoints)
            : this(parentVertices, endPoints, new DefaultStepRule())
        {

        }

        public GraphPath(IParentVertices parentVertices,
            IEndPoints endPoints, IStepRule stepRule)
        {
            path = new Lazy<IReadOnlyList<IVertex>>(GetPath);
            pathCost = new Lazy<double>(GetPathCost);
            this.parentVertices = parentVertices;
            this.endPoints = endPoints;
            this.stepRule = stepRule;
        }

        private IReadOnlyList<IVertex> GetPath()
        {
            var vertices = new List<IVertex>();
            var vertex = endPoints.Target;
            vertices.Add(vertex);
            var parent = GetOrNullVertex(vertex);
            while (parent.IsNeighbour(vertex))
            {
                vertices.Add(parent);
                vertex = parent;
                parent = GetOrNullVertex(vertex);
            }
            return vertices.AsReadOnly();
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

        private IVertex GetOrNullVertex(IVertex vertex)
        {
            return parentVertices.HasParent(vertex)
                ? parentVertices.GetParent(vertex)
                : NullVertex.Interface;
        }

        public IEnumerator<IVertex> GetEnumerator()
        {
            return Path.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}