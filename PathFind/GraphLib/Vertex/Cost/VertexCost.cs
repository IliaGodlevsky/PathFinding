using GraphLib.Vertex.Cost.CostStates;
using System;

namespace GraphLib.Vertex.Cost
{
    /// <summary>
    /// Represents a cost of vertex
    /// </summary>
    [Serializable]
    public struct VertexCost
    {
        public int CurrentCost { get; private set; }

        /// <summary>
        /// A string representing unweihted 
        /// state of vertex cost
        /// </summary>
        public string UnweightedCostView { get; set; }

        /// <summary>
        /// Creates a new instance of 
        /// <see cref="VertexCost"/>
        /// with the cost of <paramref name="startCost"/>. 
        /// Weighted cost is set to the same value
        /// </summary>
        /// <param name="startCost"></param>
        public VertexCost(int startCost)
        {
            CurrentCost = startCost;
            WeightedCost = startCost;
            Status = new WeightedState();
            UnweightedCostView = string.Empty;
        }

        /// <summary>
        /// Sets <see cref="VertexCost"/> 
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
        /// Sets <see cref="VertexCost"/> 
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
        /// of <see cref="VertexCost"/>
        /// </summary>
        /// <returns>A string representation 
        /// of <see cref="VertexCost"/></returns>
        public override string ToString()
        {
            return Status.ToString(this);
        }

        private const int UnweightedCost = 1;

        private int WeightedCost { get; set; }

        private ICostState Status { get; set; }
    }
}
