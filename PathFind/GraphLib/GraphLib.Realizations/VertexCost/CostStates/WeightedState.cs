using GraphLib.Realizations.Interfaces;
using System;

namespace GraphLib.Realizations.VertexCost.CostStates
{
    [Serializable]
    internal sealed class WeightedState : ICostState
    {
        public string ToString(Cost cost)
        {
            return cost.CurrentCost.ToString();
        }
    }
}
