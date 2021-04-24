using GraphLib.Interfaces;

namespace GraphLib.Realizations.StepRules
{
    public sealed class DefaultStepRule : IStepRule
    {
        public int StepCost(IVertex neighbour, IVertex current)
        {
            return neighbour.Cost.CurrentCost;
        }
    }
}