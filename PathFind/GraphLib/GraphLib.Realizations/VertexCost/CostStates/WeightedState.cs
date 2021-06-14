using GraphLib.Realizations.Interfaces;
using System;

namespace GraphLib.Realizations.VertexCost.CostStates
{
    [Serializable]
    internal sealed class WeightedState : ICostState
    {
        public string ToString(WeightableVertexCost cost)
        {
            return cost.CurrentCost.ToString();
        }

        public object Clone()
        {
            return new WeightedState();
        }
    }
}
