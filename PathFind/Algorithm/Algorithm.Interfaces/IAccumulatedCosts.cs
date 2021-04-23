using GraphLib.Interfaces;

namespace Algorithm.Base.CompanionClasses
{
    public interface IAccumulatedCosts
    {
        void Reevaluate(IVertex vertex, double accumulatedCost);

        double GetAccumulatedCost(IVertex vertex);

        int Compare(IVertex vertex, double accumulatedCost);
    }
}