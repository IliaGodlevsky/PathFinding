using GraphLib.Interface;
using System;

namespace GraphLib.VertexCost.CostStates
{
    [Serializable]
    internal class WeightedState : ICostState
    {
        public string ToString(Cost cost)
        {
            return cost.CurrentCost.ToString();
        }
    }
}
