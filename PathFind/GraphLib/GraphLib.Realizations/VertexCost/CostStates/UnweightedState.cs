using GraphLib.Realizations.Interfaces;
using System;

namespace GraphLib.Realizations.VertexCost.CostStates
{
    [Serializable]
    internal sealed class UnweightedState : ICostState
    {
        public string ToString(WeightableVertexCost cost)
        {
            return cost.UnweightedCostView;
        }
    }
}
