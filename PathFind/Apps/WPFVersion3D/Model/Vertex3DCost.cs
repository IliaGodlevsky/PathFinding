using Common.Extensions;
using Common.Interface;
using Common.ValueRanges;
using GraphLib.Base;
using GraphLib.Interfaces;
using System;

namespace WPFVersion3D.Model
{
    [Serializable]
    internal sealed class Vertex3DCost : BaseVertexCost, IVertexCost, ICloneable<IVertexCost>
    {
        public Vertex3DCost() : base()
        {

        }

        public Vertex3DCost(int cost) : base(cost)
        {

        }

        static Vertex3DCost()
        {
            CostRange = new InclusiveValueRange<int>(4, 1);
        }

        public override IVertexCost Clone()
        {
            return new Vertex3DCost
            {
                CurrentCost = CurrentCost
            };
        }
    }
}
