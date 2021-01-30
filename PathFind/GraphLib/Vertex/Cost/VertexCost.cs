using System;

namespace GraphLib.Vertex.Cost
{
    [Serializable]
    enum CostStatus
    {
        Unweighted,
        Weighted
    }

    [Serializable]
    public struct VertexCost : ICloneable
    {
        public int CurrentCost { get; private set; }

        public VertexCost(int startCost)
        {
            CurrentCost = startCost;
            WeightedCost = startCost;
            CostStatus = CostStatus.Weighted;
        }

        public void MakeWeighted()
        {
            CurrentCost = WeightedCost;
            CostStatus = CostStatus.Weighted;
        }

        public void MakeUnWeighted()
        {
            CurrentCost = UnweightedCost;
            CostStatus = CostStatus.Unweighted;
        }

        public string ToString(string unweightedSign = " ")
        {
            switch(CostStatus)
            {
                case CostStatus.Weighted:   return CurrentCost.ToString();
                case CostStatus.Unweighted: return unweightedSign;
                default:                    throw new Exception();
            }
        }

        public object Clone()
        {
            return new VertexCost()
            {
                CurrentCost = CurrentCost,
                WeightedCost = WeightedCost,
                CostStatus = CostStatus
            };
        }

        private CostStatus CostStatus { get; set; }

        private int UnweightedCost => 1;

        private int WeightedCost { get; set; }
    }
}
