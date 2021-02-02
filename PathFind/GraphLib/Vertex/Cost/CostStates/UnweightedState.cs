using GraphLib.Interface;
using System;

namespace GraphLib.Vertex.Cost.CostStates
{
    [Serializable]
    internal class UnweightedState : ICostState
    {
        public string ToString(VertexCost cost)
        {
            return cost.UnweightedCostView;
        }
    }
}
