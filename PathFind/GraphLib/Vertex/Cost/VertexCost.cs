using System;

namespace GraphLib.Vertex.Cost
{
    [Serializable]
    public sealed class VertexCost : ICloneable, IComparable<int>
    {
        public VertexCost(int currentCost)
        {
            CurrentCost = currentCost;
            WeightedCost = currentCost;
            IsWeighted = true;
        }

        public void MakeWeighted()
        {
            CurrentCost = WeightedCost;
            IsWeighted = true;
        }

        public void MakeUnWeighted()
        {
            CurrentCost = UnweightedCost;
            IsWeighted = false;
        }

        public static int operator +(VertexCost cost, int currentCost)
        {
            return cost.CurrentCost + currentCost;
        }

        public static int operator +(int currentCost, VertexCost cost)
        {
            return cost + currentCost;
        }

        public static int operator +(VertexCost cost1, VertexCost cost2)
        {
            return cost1.CurrentCost + cost2.CurrentCost;
        }

        public static explicit operator int(VertexCost cost)
        {
            return cost.CurrentCost;
        }

        public string ToString(string unweightedSign)
        {
            return IsWeighted ? CurrentCost.ToString() : unweightedSign;
        }

        public object Clone()
        {
            return new VertexCost()
            {
                CurrentCost = CurrentCost,
                WeightedCost = WeightedCost,
                IsWeighted = IsWeighted
            };
        }

        public int CompareTo(int other)
        {
            return CurrentCost.CompareTo(other);
        }

        private VertexCost()
        {

        }

        private bool IsWeighted { get; set; }

        private int CurrentCost { get; set; }

        private int UnweightedCost => 1;

        private int WeightedCost { get; set; }
    }
}
