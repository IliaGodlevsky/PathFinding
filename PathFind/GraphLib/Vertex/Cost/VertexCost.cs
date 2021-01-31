using System;

namespace GraphLib.Vertex.Cost
{
    [Serializable]
    public struct VertexCost
    {
        public int CurrentCost { get; private set; }

        public VertexCost(int startCost)
        {
            CurrentCost = startCost;
            WeightedCost = startCost;
            Status = CostStatus.Weighted;
        }

        public void MakeWeighted()
        {
            CurrentCost = WeightedCost;
            Status = CostStatus.Weighted;
        }

        public void MakeUnWeighted()
        {
            CurrentCost = UnweightedCost;
            Status = CostStatus.Unweighted;
        }

        public string ToString(string unweightedSign = "")
        {
            switch (Status)
            {
                case CostStatus.Weighted:
                    return CurrentCost.ToString();
                case CostStatus.Unweighted:
                    return unweightedSign;
                default:
                    throw new Exception("Status isn't processed");
            }
        }

        private int UnweightedCost => 1;

        private int WeightedCost { get; set; }

        [Serializable]
        private enum CostStatus
        {
            Unweighted,
            Weighted
        }

        private CostStatus Status { get; set; }
    }
}
