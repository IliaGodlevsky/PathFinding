using Algorithm.Interfaces;
using Common.Extensions;
using GraphLib.Interfaces;

namespace Algorithm.Сompanions
{
    public sealed class AccumulatedCostsWithExcept : IAccumulatedCosts
    {
        public AccumulatedCostsWithExcept(IAccumulatedCosts accumulatedCosts,
            params IVertex[] except)
        {
            this.accumulatedCosts = accumulatedCosts;
            except.ForEach(SetAccumulatedCostToDefault);
        }


        public void Reevaluate(IVertex vertex, double accumulatedCost)
        {
            accumulatedCosts.Reevaluate(vertex, accumulatedCost);
        }

        public double GetAccumulatedCost(IVertex vertex)
        {
            return accumulatedCosts.GetAccumulatedCost(vertex);
        }

        public int Compare(IVertex vertex, double accumulatedCost)
        {
            return accumulatedCosts.Compare(vertex, accumulatedCost);
        }

        private void SetAccumulatedCostToDefault(IVertex vertex)
        {
            accumulatedCosts.Reevaluate(vertex, default);
        }

        private readonly IAccumulatedCosts accumulatedCosts;
    }
}