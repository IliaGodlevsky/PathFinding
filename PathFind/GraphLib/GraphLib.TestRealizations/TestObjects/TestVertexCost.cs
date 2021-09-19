using Common.Interface;
using GraphLib.Base;
using GraphLib.Interfaces;
using System;

namespace GraphLib.TestRealizations.TestObjects
{
    [Serializable]
    public sealed class TestVertexCost : BaseVertexCost, IVertexCost, ICloneable<IVertexCost>
    {
        public TestVertexCost(int cost)
            : base(cost)
        {

        }

        public TestVertexCost() : base()
        {

        }

        public override string ToString()
        {
            return CurrentCost.ToString();
        }

        public override IVertexCost Clone()
        {
            return new TestVertexCost(CurrentCost);
        }
    }
}
