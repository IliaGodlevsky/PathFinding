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
    /// <summary>
    /// A class that contains the 
    /// result of pathfinding process.
    /// This class can't be inherited
    /// </summary>
    public sealed class GraphPath : IGraphPath
    {
        /// <summary>
        /// Vertices, that represent path. The vertices are ordered
        /// in the same way they were visited by an algorithm
        /// </summary>
        public IReadOnlyList<IVertex> Path => path.Value;

        /// <summary>
        /// Cost of the past that is calculated according to a step rule
        /// that was used to calculate the cost of step
        /// </summary>
        public double Cost => pathCost.Value;

        public int Length => pathLength.Value;

        /// <summary>
        /// Initializes a new instance of <see cref="GraphPath"/>
        /// using <see cref="DefaultStepRule"/> as step rule
        /// </summary>
        /// <param name="parentVertices">A class, that contains pairs of vertices. 
        /// Every pair contains a vertex and a vertex, from which is was visited</param>
        /// <param name="endPoints">End points, between which the path was found</param>
        public GraphPath(IParentVertices parentVertices, IEndPoints endPoints)
            : this(parentVertices, endPoints, new DefaultStepRule())
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="GraphPath"/>
        /// </summary>
        /// <param name="parentVertices">A class, that contains pairs of vertices. 
        /// Every pair contains a vertex and a vertex, from which is was visited</param>
        /// <param name="endPoints">End points, between which the path was found</param>
        /// <param name="stepRule">A rule, that was used to calculate the 
        /// cost step between vertices</param>
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

        /// <summary>
        /// Extracts path from <see cref="IVisitedVertices"/>
        /// </summary>
        /// <returns>Extracted path or empty path 
        /// if there is no source vertex</returns>
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
