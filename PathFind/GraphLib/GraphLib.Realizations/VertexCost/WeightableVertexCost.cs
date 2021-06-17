using Common.Extensions;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Realizations.Interfaces;
using GraphLib.Realizations.VertexCost.CostStates;
using System;

namespace GraphLib.Realizations.VertexCost
{
    /// <summary>
    /// Represents a cost of vertex
    /// </summary>
    [Serializable]
    public sealed class WeightableVertexCost 
        : BaseVertexCost, IWeightable
    {
        /// <summary>
        /// Creates a new instance of 
        /// <see cref="WeightableVertexCost"/>
        /// with the cost of <paramref name="startCost"/>. 
        /// Weighted cost is set to the same value
        /// </summary>
        /// <param name="startCost"></param>
        public WeightableVertexCost(int startCost)
            : base(startCost)
        {
            WeightedCost = CurrentCost;
            Status = new WeightedState();
            UnweightedCostView = string.Empty;
        }

        /// <summary>
        /// Creates a new instance of 
        /// <see cref="WeightableVertexCost"/>
        /// with random cost.
        /// Weighted cost is set to the same value
        /// </summary>
        public WeightableVertexCost()
            : this(CostRange.GetRandomValue())
        {

        }

        /// <summary>
        /// A string representing unweighted 
        /// state view of vertex cost
        /// </summary>
        public string UnweightedCostView { get; set; }

        /// <summary>
        /// Sets <see cref="WeightableVertexCost"/> 
        /// to weighted status. That means that
        /// the current cost of vertex will be 
        /// set to its normal value
        /// </summary>
        public void MakeWeighted()
        {
            CurrentCost = WeightedCost;
            Status = new WeightedState();
        }

        /// <summary>
        /// Sets <see cref="WeightableVertexCost"/> 
        /// to unweighted status.
        /// It means that the current cost of 
        /// vertex will be set to 1
        /// </summary>
        public void MakeUnweighted()
        {
            CurrentCost = UnweightedCost;
            Status = new UnweightedState();
        }

        /// <summary>
        /// Returns a string representation 
        /// of <see cref="WeightableVertexCost"/>
        /// </summary>
        /// <returns>A string representation 
        /// of <see cref="WeightableVertexCost"/></returns>
        public override string ToString()
        {
            return Status.ToString(this);
        }

        public override object Clone()
        {
            return new WeightableVertexCost(CurrentCost)
            {
                WeightedCost = WeightedCost,
                Status = (ICostState)Status.Clone(),
                UnweightedCostView = UnweightedCostView
            };
        }

        private int WeightedCost { get; set; }
        private ICostState Status { get; set; }

        private const int UnweightedCost = 1;
    }
}