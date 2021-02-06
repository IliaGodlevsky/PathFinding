using Common;
using GraphLib.Interface;
using GraphLib.VertexCost.CostStates;
using System;

namespace GraphLib.VertexCost
{
    /// <summary>
    /// Represents a cost of vertex
    /// </summary>
    [Serializable]
    public sealed class Cost : ICloneable
    {
        /// <summary>
        /// Creates a new instance of 
        /// <see cref="Cost"/>
        /// with the cost of <paramref name="startCost"/>. 
        /// Weighted cost is set to the same value
        /// </summary>
        /// <param name="startCost"></param>
        public Cost(int startCost)
        {
            startCost = CostRange.ReturnInRange(startCost);
            CurrentCost = startCost;
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
        /// <param name="startCost"></param>
        public Cost() : this(CostRange.GetRandomValueFromRange())
        {

        }

        static Cost()
        {
            CostRange = new ValueRange(9, 1);
        }

        public static ValueRange CostRange { get; set; }

        public int CurrentCost { get; private set; }

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

        public override bool Equals(object obj)
        {
            if (obj is Cost cost)
            {
                return cost.CurrentCost == CurrentCost;
            }

            var message = "An error was occured while comparing\n";
            message += $"an instance of {nameof(Cost)} and {obj.GetType().Name}\n";
            throw new ArgumentException(message, nameof(obj));
        }

        public override int GetHashCode()
        {
            return CurrentCost.GetHashCode();
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

        public object Clone()
        {
            return new Cost
            {
                CurrentCost = CurrentCost,
                Status = (ICostState)Status.Clone(),
                UnweightedCostView = UnweightedCostView,
                WeightedCost = WeightedCost
            };
        }

        private int WeightedCost { get; set; }
        private ICostState Status { get; set; }

        private const int UnweightedCost = 1;
    }
}