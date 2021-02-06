using GraphLib.Interface;
using System;

namespace GraphLib.VertexCost.CostStates
{
    [Serializable]
    internal class UnweightedState : ICostState
    {
        public object Clone()
        {
            return new UnweightedState();
        }

        public string ToString(Cost cost)
        {
            return cost.UnweightedCostView;
        }
    }
}
