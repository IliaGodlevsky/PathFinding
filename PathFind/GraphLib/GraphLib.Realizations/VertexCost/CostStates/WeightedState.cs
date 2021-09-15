using Common.Interface;
using GraphLib.Realizations.Interfaces;
using System;

namespace GraphLib.Realizations.VertexCost.CostStates
{
    [Serializable]
    internal sealed class WeightedState : ICostState, ICloneable<ICostState>
    {
        public string ToString(WeightableVertexCost cost)
        {
            return cost.CurrentCost.ToString();
        }

        public ICostState Clone()
        {
            return new WeightedState();
        }
    }
}
