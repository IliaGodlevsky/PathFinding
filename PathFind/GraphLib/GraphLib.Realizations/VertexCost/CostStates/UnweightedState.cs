using Common.Interface;
using GraphLib.Realizations.Interfaces;
using System;

namespace GraphLib.Realizations.VertexCost.CostStates
{
    [Serializable]
    internal sealed class UnweightedState : ICostState, ICloneable<ICostState>
    {
        public string ToString(WeightableVertexCost cost)
        {
            return cost.UnweightedCostView;
        }

        public ICostState Clone()
        {
            return new UnweightedState();
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
