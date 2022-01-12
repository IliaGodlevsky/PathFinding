using Common.Interface;
using GraphLib.Base;
using GraphLib.Interfaces;
using System;
using System.Diagnostics;
using ValueRange;

namespace WPFVersion3D.Model
{
    [Serializable]
    [DebuggerDisplay("Cost = {CurrentCost}")]
    internal sealed class Vertex3DCost : BaseVertexCost, IVertexCost, ICloneable<IVertexCost>
    {
        public Vertex3DCost(int cost) : base(cost)
        {

        }

        static Vertex3DCost()
        {
            CostRange = new InclusiveValueRange<int>(4, 1);
        }

        public override IVertexCost Clone()
        {
            return new Vertex3DCost(CurrentCost);
        }
    }
}
