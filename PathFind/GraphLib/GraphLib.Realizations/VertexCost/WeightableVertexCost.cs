using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Realizations.Interfaces;
using GraphLib.Realizations.VertexCost.CostStates;
using System;
using System.Diagnostics;

namespace GraphLib.Realizations.VertexCost
{
    [Serializable]
    [DebuggerDisplay("Cost = {CurrentCost}")]
    public sealed class WeightableVertexCost : BaseVertexCost, IWeightable
    {
        private const int UnweightedCost = 1;

        public string UnweightedCostView { get; set; }

        private int WeightedCost { get; set; }

        private ICostState Status { get; set; }

        public WeightableVertexCost(int startCost)
            : base(startCost)
        {
            WeightedCost = CurrentCost;
            Status = new WeightedState();
            UnweightedCostView = string.Empty;
        }

        public void MakeWeighted()
        {
            CurrentCost = WeightedCost;
            Status = new WeightedState();
        }

        public void MakeUnweighted()
        {
            CurrentCost = UnweightedCost;
            Status = new UnweightedState();
        }

        public override string ToString()
        {
            return Status.ToString(this);
        }

        public override IVertexCost Clone()
        {
            return new WeightableVertexCost(CurrentCost)
            {
                WeightedCost = WeightedCost,
                Status = Status.Clone(),
                UnweightedCostView = UnweightedCostView
            };
        }
    }
}