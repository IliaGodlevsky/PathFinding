using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using Algorithm.Realizations.StepRules;
using AssembleClassesLib.Attributes;
using GraphLib.Interfaces;

namespace Plugins.AngleAStarAlgorithm
{
    [ClassName("A* algorithm (angle version)")]
    public class AngleAStarAlgorithm : AStarAlgorithm.AStarAlgorithm
    {
        public AngleAStarAlgorithm(IGraph graph, IEndPoints endPoints) 
            : base(graph, endPoints, new DefaultStepRule(), new Angle(endPoints.Source))
        {

        }

        public AngleAStarAlgorithm(IGraph graph, IEndPoints endPoints, IStepRule stepRule) 
            : base(graph, endPoints, stepRule, new Angle(endPoints.Source))
        {

        }
    }
}
