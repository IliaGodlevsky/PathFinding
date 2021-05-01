using Algorithm.Interfaces;
using Algorithm.Realizations.StepRules;
using Common.Extensions;
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
            IEndPoints fromEndEndPoints, IStepRule stepRule)
        {
            this.fromStartPath = fromStartPath;
            this.fromEndPath = fromEndPath;
            this.stepRule = stepRule;
            endPoints = fromEndEndPoints;
        }

        public IEnumerable<IVertex> Path
        {
            get
            {
                if (path == null)
                {
                    path = fromStartPath.Path
                        .Concat(fromEndPath.Path)
                        .Append(endPoints.Start)
                        .DistinctBy(Position)
                        .ToArray();

                    PathCost = fromStartPath.PathCost 
                        + fromEndPath.PathCost + AdjustPathCost();
                }
                return path;
            }
        }

        public double PathCost { get; private set; }

        private double AdjustPathCost()
        {
            return stepRule.CalculateStepCost(endPoints.Start, endPoints.Start)
                   - stepRule.CalculateStepCost(endPoints.End, endPoints.End);
        }

        private ICoordinate Position(IVertex vertex) => vertex.Position;

        private readonly IGraphPath fromStartPath;
        private readonly IGraphPath fromEndPath;
        private IVertex[] path;

        private readonly IStepRule stepRule;
        private readonly IEndPoints endPoints;
    }
}
