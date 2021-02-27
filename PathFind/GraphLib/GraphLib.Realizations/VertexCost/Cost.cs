using GraphLib.Base;
using GraphLib.Realizations.Interfaces;
using GraphLib.Realizations.VertexCost.CostStates;
using System;

namespace GraphLib.Realizations.VertexCost
{
    /// <summary>
    /// Represents a cost of vertex
    /// </summary>
    [Serializable]
    public sealed class Cost : BaseVertexCost
    {
        /// <summary>
        /// Creates a new instance of 
        /// <see cref="Cost"/>
        /// with the cost of <paramref name="startCost"/>. 
        /// Weighted cost is set to the same value
        /// </summary>
        /// <param name="startCost"></param>
        public Cost(int startCost) : base(startCost)
        {
            WeightedCost = startCost;
            Status = new WeightedState();
            UnweightedCostView = string.Empty;
        }

        /// <summary>
        /// Creates a new instance of 
        /// <see cref="Cost"/>
        /// with random cost.
        /// Weighted cost is set to the same value
        /// </summary>
        public Cost() : this(CostRange.GetRandomValueFromRange())
        {

        }

        /// <summary>
        /// A string representing unweighted 
        /// state of vertex cost
        /// </summary>
        public string UnweightedCostView { get; set; }

        /// <summary>
        /// Sets <see cref="Cost"/> 
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
        /// Sets <see cref="Cost"/> 
        /// to unweighted status.
        /// It means that the current cost of 
        /// vertex will be set to 1
        /// </summary>
        public void MakeUnWeighted()
        {
            CurrentCost = UnweightedCost;
            Status = new UnweightedState();
        }

        /// <summary>
        /// Returns a string representation 
        /// of <see cref="Cost"/>
        /// </summary>
        /// <returns>A string representation 
        /// of <see cref="Cost"/></returns>
        public override string ToString()
        {
            return Status.ToString(this);
        }

        private int WeightedCost { get; set; }
        private ICostState Status { get; set; }

        private const int UnweightedCost = 1;
    }
}