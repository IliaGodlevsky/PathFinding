using GraphLib.Interfaces;

namespace Algorithm.Сompanions.Interface
{
    public interface ICosts
    {
        bool Contains(IVertex vertex);

        void Reevaluate(IVertex vertex, double accumulatedCost);

        double GetCost(IVertex vertex);

        void Clear();
    }
}