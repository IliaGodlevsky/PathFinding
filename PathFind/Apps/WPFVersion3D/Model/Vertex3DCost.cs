using GraphLib.Base;
using System.Diagnostics;
using ValueRange;

namespace WPFVersion3D.Model
{
    [DebuggerDisplay("Cost = {CurrentCost}")]
    internal sealed class Vertex3DCost : BaseVertexCost
    {
        public Vertex3DCost(int cost) : base(cost)
        {

        }

        static Vertex3DCost()
        {
            CostRange = new InclusiveValueRange<int>(4, 1);
        }
    }
}
