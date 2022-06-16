using Algorithm.Сompanions.Interface;
using GraphLib.Interfaces;

namespace Algorithm.Extensions
{
    public static class ICostsExtensions
    {
        public static double GetCostOrDefault(this ICosts costs, IVertex vertex, double defaultValue = double.PositiveInfinity)
        {
            return costs.Contains(vertex) ? costs.GetCost(vertex) : defaultValue;
        }
    }
}
