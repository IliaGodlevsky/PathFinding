using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using Common.Extensions;
using GraphLib.Common.NullObjects;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Realizations.GraphPaths
{
    public sealed class CombinedGraphPath : IGraphPath
    {
        public CombinedGraphPath(IGraphPath fromStartPath, IGraphPath fromEndPath, IEndPoints endPoints)
            : this(fromStartPath, fromEndPath, endPoints, new DefaultStepRule())
        {

        }

        public CombinedGraphPath(IGraphPath fromStartPath, IGraphPath fromEndPath,
            IEndPoints endPoints, IStepRule stepRule)
        {
            this.fromStartPath = fromStartPath;
            this.fromEndPath = fromEndPath;
            this.stepRule = stepRule;
            this.endPoints = endPoints;
        }

        public IEnumerable<IVertex> Path
        {
            get
            {
                if (path == null)
                {
                    var intersect = fromStartPath.Path
                        .Intersect(fromStartPath.Path)
                        .FirstOrDefault() ?? new NullVertex();

                    path = fromStartPath.Path
                        .Concat(fromEndPath.Path)
                        .Append(endPoints.End)
                        .DistinctBy(vertex => vertex.Position)
                        .ToArray();

                    PathCost = fromStartPath.PathCost + fromEndPath.PathCost + AdjustPathCost(intersect);
                }
                return path;
            }
        }

        private double AdjustPathCost(IVertex intersect)
        {
            return stepRule.CalculateStepCost(endPoints.End, endPoints.End)
                        - stepRule.CalculateStepCost(intersect, intersect);
        }

        public double PathCost { get; private set; }

        private readonly IGraphPath fromStartPath;
        private readonly IGraphPath fromEndPath;
        private IVertex[] path;

        private readonly IStepRule stepRule;
        private readonly IEndPoints endPoints;
    }
}
