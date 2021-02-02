using GraphLib.Interface;
using System;

namespace GraphLib.Vertex.Cost.CostStates
{
    [Serializable]
    internal class WeightedState : ICostState
    {
        public string ToString(VertexCost cost)
        {
            return cost.CurrentCost.ToString();
        }
    }
}
