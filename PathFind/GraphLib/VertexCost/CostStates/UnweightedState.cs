using GraphLib.Interface;
using System;

namespace GraphLib.VertexCost.CostStates
{
    [Serializable]
    internal class UnweightedState : ICostState
    {
        public string ToString(Cost cost)
        {
            return cost.UnweightedCostView;
        }
    }
}
