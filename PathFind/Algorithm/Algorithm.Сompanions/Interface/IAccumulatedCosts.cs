using GraphLib.Interfaces;

namespace Algorithm.Сompanions.Interface
{
    public interface IAccumulatedCosts
    {
        void Reevaluate(IVertex vertex, double accumulatedCost);

        double GetAccumulatedCost(IVertex vertex);

        int Compare(IVertex vertex, double accumulatedCost);
    }
}